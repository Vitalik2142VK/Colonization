using System.Collections.Generic;
using UnityEngine;

public abstract class MoverUnit : MonoBehaviour 
{
    protected const float RadiusReachingPoint = 1.0f;

    [SerializeField, Min(0)] private float _speed;

    private Queue<Transform> _waipoints;
    private Transform _currentPoint;
    private bool _isThereWaypoint = false;

    public bool IsThereWaypoint => _isThereWaypoint;

    protected float Speed => _speed;

    public void SetWaypoints(Queue<Transform> waypints)
    {
        _waipoints = waypints;
        _isThereWaypoint = true;

        AppointNextPoint();
    }

    protected void MoveToPoint()
    {
        if (IsThereWaypoint)
        {
            Vector3 waypoint = _currentPoint.position;
            float radiusReachingPoint = RadiusReachingPoint + _currentPoint.localScale.x; // создать класс waypoints, который содержит похицию и радиус завершения?

            if (Vector3.Distance(waypoint, transform.position) > radiusReachingPoint)
                transform.position = Vector3.MoveTowards(transform.position, waypoint, _speed * Time.deltaTime);
            else
                AppointNextPoint();
        }
    }

    private void AppointNextPoint()
    {
        if (_waipoints.Count != 0)
            _currentPoint = _waipoints.Dequeue();
        else
            _isThereWaypoint = false;

        Debug.Log($"Appoint Next Point: {_currentPoint.position}"); // delete
    }

    public abstract void Move();
}
