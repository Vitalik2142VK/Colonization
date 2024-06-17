using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ListBaseUnits))]
public class BaseUnitsController : MonoBehaviour 
{
    private ListBaseUnits _listUnits;

    private void Awake()
    {
        _listUnits = GetComponent<ListBaseUnits>();
    }

    public void AddUnit(Unit unit)
    {
        _listUnits.AddUnit(unit);
        unit.StopMove();
    }

    public void SendWorkers(List<ResourcePlace> resourcesPlasces)
    {
        Queue<Worker> collectors = _listUnits.GetFreeWorkers();

        int currentIndexResource = 0;

        while (collectors.Count > 0 && resourcesPlasces.Count > 0)
        {
            Worker collector = collectors.Dequeue();
            ResourcePlace resourcePlace = resourcesPlasces[currentIndexResource];
            Transform directionPoint = resourcePlace.transform;
            Queue<Waypoint> waypoints = SpecifyWaypointAndBase(directionPoint);

            collector.SetWaypoints(waypoints);
            collector.SetResourcePlace(resourcePlace);

            currentIndexResource = ++currentIndexResource % resourcesPlasces.Count;
        }
    }

    public void SendCollectorByWorker(Worker collector, ResourcePlace resourcePlace)
    {
        Queue<Waypoint> waypoints = SpecifyWaypointAndBase(resourcePlace.transform);
        collector.SetResourcePlace(resourcePlace);
        collector.SetWaypoints(waypoints);
        collector.PutResource();
    }

    public bool TrySentToNewBase(out Worker collector, Transform pointNewBase)
    {
        _listUnits.TryGiveFreeWorker(out collector);

        if (collector == null)
            return false;

        collector.SetWaypoints(SpecifyWaypoint(pointNewBase));

        return true;
    }

    private Queue<Waypoint> SpecifyWaypointAndBase(Transform point)
    {
        Queue<Waypoint> waypoints = SpecifyWaypoint(point);
        float radiusReachingBase = 20.0f;

        waypoints.Enqueue(new Waypoint(transform, radiusReachingBase));

        return waypoints;
    }

    private Queue<Waypoint> SpecifyWaypoint(Transform point)
    {
        Queue<Waypoint> waypoints = new Queue<Waypoint>();
        waypoints.Enqueue(new Waypoint(point));

        return waypoints;
    }
}
