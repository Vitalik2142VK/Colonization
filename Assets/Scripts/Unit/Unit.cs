using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoverUnit), typeof(VisionUnit))]
public abstract class Unit : MonoBehaviour, IUnit
{
    private MoverUnit _moverUnit;
    private VisionUnit _visionUnit;

    public bool IsBusy => _moverUnit.IsThereWaypoint;

    protected MoverUnit MoverUnit => _moverUnit;
    protected VisionUnit VisionUnit => _visionUnit;

    public void SetWaypoints(Queue<Waypoint> waypoints)
    {
        _moverUnit.SetWaypoints(waypoints);
    }

    public void StopMove()
    {
        _moverUnit.RemoveWaipoints();
    }

    protected void GetComponents()
    {
        _moverUnit = GetComponent<MoverUnit>();
        _visionUnit = GetComponent<VisionUnit>();
    }

    public abstract Dictionary<string, int> GetRequiredResources();
}
