using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoverUnit))]
public abstract class Unit : MonoBehaviour, IUnit
{
    private MoverUnit _moverUnit;

    public bool IsBusy => _moverUnit.IsThereWaypoint;

    protected MoverUnit MoverUnit => _moverUnit;

    public void SetWaypoints(Queue<Transform> waypoints)
    {
        _moverUnit.SetWaypoints(waypoints);
    }

    protected void Move()
    {
        _moverUnit.Move();
    }

    protected void GetComponents()
    {
        _moverUnit = GetComponent<MoverUnit>();
    }
}
