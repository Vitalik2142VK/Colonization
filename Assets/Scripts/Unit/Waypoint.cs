using UnityEngine;

public class Waypoint
{
    private const float DefaultRadiusReachingPoint = 1.0f;

    private Vector3 _directionPoint;
    private float _radiusReachingPoint;

    public Waypoint(Transform point, float radiusReachingPoint = DefaultRadiusReachingPoint)
    {
        _directionPoint = point.position;

        float radiusByScale;

        if (point.localScale.x >= point.localScale.z)
            radiusByScale = point.localScale.x;
        else
            radiusByScale = point.localScale.z;

        _radiusReachingPoint = radiusReachingPoint + radiusByScale;
    }

    public Vector3 DirectionPoint => _directionPoint;

    public bool IsPointReached(Vector3 currentPosition)
    {
        return Vector3.Distance(_directionPoint, currentPosition) < _radiusReachingPoint;
    }
}
