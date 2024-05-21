using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceScaner))]
[RequireComponent(typeof(ListBasicUnits))]
public class Base : Building
{
    [SerializeField] private ResourceCounter[] _resourceCounters;
    [SerializeField] private ResourceCollectionArea _resourceCollectionArea;

    public event Action<Resource> ResourceDelivered;

    private ResourceScaner _resouceScaner;
    private ListBasicUnits _listUnits;

    private void Awake()
    {
        _resouceScaner = GetComponent<ResourceScaner>();
        _listUnits = GetComponent<ListBasicUnits>();
    }

    private void OnEnable()
    {
        _resouceScaner.ResourcesFound += OnSendCollectors;
        _resourceCollectionArea.ResourceDelivered += OnResourceDelivered;
    }

    private void OnDisable()
    {
        _resouceScaner.ResourcesFound -= OnSendCollectors;
        _resourceCollectionArea.ResourceDelivered -= OnResourceDelivered;
    }

    private void OnSendCollectors(List<ResourcePlace> foundResources)
    {
        if (foundResources.Count == 0)
            return;

        Queue<Collector> collectors = _listUnits.GetFreeCollectors();
        List<ResourcePlace> resourcesPlasces = GetNearestResources(foundResources);
        int currentIndexResource = 0;

        while (collectors.Count > 0 && resourcesPlasces.Count > 0)
        {
            Collector collector = collectors.Dequeue();
            ResourcePlace resourcePlace = resourcesPlasces[currentIndexResource];
            Transform directionPoint = resourcePlace.transform;
            Queue<Waypoint> waypoints = SpecifyWaypoints(directionPoint);

            collector.SetWaypoints(waypoints);
            collector.SetResourcePlace(resourcePlace);

            currentIndexResource = ++currentIndexResource % resourcesPlasces.Count;
        }
    }

    private void OnResourceDelivered(Collector collector, Resource resource)
    {
        Debug.Log($"OnResourceDelivered on Base {resource}"); // delete

        SendCollector(collector);

        ResourceDelivered?.Invoke(resource);
    }

    private List<ResourcePlace> GetNearestResources(List<ResourcePlace> foundResources)
    {
        List<ResourcePlace> resources = new List<ResourcePlace>();

        foreach (var resourceCounter in _resourceCounters)
        {
            if (resourceCounter.IsFull == false && foundResources.Count > 0)
            {
                List<ResourcePlace> placesSameType = foundResources
                    .Where(rp => resourceCounter.IsResourceSuitable(rp))
                    .ToList();

                if (placesSameType.Count == 0) 
                    break;

                float distance = placesSameType.Min(r => Vector3.Distance(r.transform.position, transform.position));

                ResourcePlace resource = placesSameType
                    .Where(r => distance == Vector3.Distance(r.transform.position, transform.position))
                    .First();

                if (resource != null)
                    resources.Add(resource);
            }
        }

        return resources;
    }

    private void SendCollector(Collector collector)
    {
        ResourceCounter suitableResourceCounter = _resourceCounters
            .Where(rc => rc.IsResourceSuitable(collector.ResourcePlace))
            .FirstOrDefault(rc => rc = null);

        //foreach (var resourceCounter in _resourceCounters)
        //{
        //    Debug.Log($"IsResourceSuitable - {resourceCounter.IsResourceSuitable(collector.ResourcePlace)}");

        //    if (resourceCounter.IsResourceSuitable(collector.ResourcePlace)) 
        //    {
        //        suitableResourceCounter = resourceCounter;

        //        break;
        //    }
        //}

        Debug.Log($"ResourceCounter - {suitableResourceCounter}");

        if (suitableResourceCounter == null)
            return;

        if (suitableResourceCounter.IsFull == false && collector.IsResourcePlacedepleted() == false)
        {
            Queue<Waypoint> waypoints = SpecifyWaypoints(collector.ResourcePlace.transform);
            collector.SetWaypoints(waypoints);
        }
    }

    private Queue<Waypoint> SpecifyWaypoints(Transform point)
    {
        Queue<Waypoint> waypoints = new Queue<Waypoint>();
        float radiusReachingBase = 20.0f;

        waypoints.Enqueue(new Waypoint(point));
        waypoints.Enqueue(new Waypoint(transform, radiusReachingBase));

        return waypoints;
    }
}
