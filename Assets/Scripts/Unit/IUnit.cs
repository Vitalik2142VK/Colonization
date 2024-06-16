using System.Collections.Generic;

public interface IUnit : IInteractive
{
    public void SetWaypoints(Queue<Waypoint> waypints);
}
