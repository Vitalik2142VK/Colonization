using System.Collections.Generic;
using UnityEngine;

public interface IUnit 
{
    public void SetWaypoints(Queue<Transform> waypints);
}
