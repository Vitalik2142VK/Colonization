using System.Collections.Generic;
using UnityEngine;

public abstract class MoverUnit : MonoBehaviour
{
    protected const float RadiusReachingPoint = 1.0f;

    [SerializeField, Min(3.0f)] private float _speed;
    [SerializeField, Min(1.0f)] private float _timeWaitPoint;

    private Timer _timerWait;
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
        if (_timerWait != null)
            return;

        _timerWait = new Timer(_timeWaitPoint);
        _timerWait.UpdateWaitingTime();
    }

    private void WaitNextPoint()
    {
        if (_timerWait.IsTimeUp)
        {
            _timerWait.UpdateWaitingTime();

            AppointNextPoint();
        }
        else
            _timerWait.MakeCountdown();
    }

    public abstract void Move();
}
