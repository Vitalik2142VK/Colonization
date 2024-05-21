using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoverUnit : MonoBehaviour
{
    protected const float RadiusReachingPoint = 1.0f;

    [SerializeField, Min(3.0f)] private float _speed;
    [SerializeField, Min(1.0f)] private float _timeWaitPoint;

    private Timer _timer; // don't use right now;
    private Queue<Waypoint> _waipoints;
    private Waypoint _currentPoint;
    private bool _isThereWaypoint = false;

    public bool IsThereWaypoint => _isThereWaypoint;

    protected float Speed => _speed;

    public void SetWaypoints(Queue<Waypoint> waypints)
    {
        CreateTimer();

        _waipoints = waypints;
        _isThereWaypoint = true;

        AppointNextPoint();
    }

    protected void MoveToPoint()
    {
        if (IsThereWaypoint)
        {
            if (_currentPoint.IsPointReached(transform.position) == false)
                transform.position = Vector3.MoveTowards(transform.position, _currentPoint.DirectionPoint, _speed * Time.deltaTime);
            else
                WaitNextPoint();
        }
    }

    protected void EstablishLastPoint()
    {
        int numberLastPoint = 1;

        while (_waipoints.Count >= numberLastPoint)
        {
            AppointNextPoint();
        }
    }

    private void AppointNextPoint()
    {
        if (_waipoints.Count != 0)
            _currentPoint = _waipoints.Dequeue();
        else
            _isThereWaypoint = false;
    }

    private void CreateTimer()
    {
        if (_timer != null)
            return;

        _timer = new Timer(_timeWaitPoint);
        _timer.UpdateWaitingTime();
    }

    private void WaitNextPoint()
    {
        if (_timer.IsTimeUp)
        {
            _timer.UpdateWaitingTime();

            AppointNextPoint();
        }
        else
            _timer.MakeCountdown();
    }

    public abstract void Move();
}
