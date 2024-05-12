using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceScaner))]
[RequireComponent(typeof(ListBasicUnits))]
public class Base : Building
{
    [SerializeField] private ResourceCounter[] _resourceCounters;

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
    }

    private void OnDisable()
    {
        _resouceScaner.ResourcesFound -= OnSendCollectors;
    }

    private void OnSendCollectors(List<ResourcePlace> foundResources)
    {
        Debug.Log($"Data foundResources send {foundResources.Count}"); // delete

        if (foundResources.Count == 0)
            return;

        Queue<Collector> collectors = _listUnits.GetFreeCollectors();
        List<ResourcePlace> resourcesPlasces = GetNearestResources(foundResources);
        int currentIndexResource = 0;

        while (collectors.Count > 0)
        {
            Collector collector = collectors.Dequeue();
            Transform waypoint = resourcesPlasces[currentIndexResource].transform;

            Queue<Transform> waypoints = SpecifyWaypoints(waypoint);
            collector.SetWaypoints(waypoints);

            currentIndexResource = ++currentIndexResource % resourcesPlasces.Count;
        }
    }

    private List<ResourcePlace> GetNearestResources(List<ResourcePlace> foundResources)
    {
        List<ResourcePlace> resources = new List<ResourcePlace>();

        foreach (var resourceCounter in _resourceCounters)
        {
            if (resourceCounter.IsFull == false)
            {
                foreach (var resource in foundResources)
                {
                    Debug.Log($"FoundResources: {resource}"); // delete
                    Debug.Log($"CheckResourceSuitable: {resourceCounter.IsResourceSuitable(resource)}"); // delete

                    if (resourceCounter.IsResourceSuitable(resource))
                        resources.Add(resource);
                }
            }
        }

        Debug.Log($"NearestResources: {resources.Count}"); // delete

        return resources;
    }

    private Queue<Transform> SpecifyWaypoints(Transform point)
    {
        Queue<Transform> waypoints = new Queue<Transform>();

        waypoints.Enqueue(point);
        waypoints.Enqueue(transform);

        return waypoints;
    }
}
