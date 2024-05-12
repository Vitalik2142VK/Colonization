using System.Collections.Generic;
using UnityEngine;

public class ListBasicUnits : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;

    public Queue<Collector> GetFreeCollectors()
    {
        Queue<Collector> collectors = new Queue<Collector>();

        foreach (var unit in _units) 
        {
            if (unit is Collector collector && unit.IsBusy == false)
                collectors.Enqueue(collector);
        }

        return collectors;
    }
}
