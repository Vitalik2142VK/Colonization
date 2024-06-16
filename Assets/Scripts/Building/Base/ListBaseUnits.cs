﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListBaseUnits : MonoBehaviour
{
    private const string ContainerUnits = nameof(ContainerUnits);

    [SerializeField] private List<Unit> _units;

    private Transform _container;

    private void Awake()
    {
        CreateContainerUnits();
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);

        AddUnitToContaincer(unit);
        ChangeColorUnit(unit);
    }

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

    public bool TryGiveFreeCollector(out Collector collector)
    {
        collector = _units
            .Where(u => u is Collector && u.IsBusy == false)
            .Select(u => (Collector)u)
            .FirstOrDefault();

        if (collector == null)
            return false;

        _units.Remove(collector);
        collector.transform.parent = null;

        return true;
    }

    private void CreateContainerUnits()
    {
        GameObject gameObjectContainer = new GameObject(ContainerUnits + gameObject);
        _container = gameObjectContainer.transform;

        if (_units.Count > 0)
        {
            foreach (var unit in _units)
            {
                AddUnitToContaincer(unit);
                ChangeColorUnit(unit);
            }
        }
    }

    private void AddUnitToContaincer(Unit unit)
    {
        unit.transform.parent = _container;
    }

    private void ChangeColorUnit(Unit unit)
    {
        Color color = GetComponent<Renderer>().material.color;

        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out Renderer rendererChild))
                {
                    color = rendererChild.material.color;
                    break;
                }
            }
        }

        if (unit.TryGetComponent(out Renderer renderer))
            renderer.material.color = color;
    }
}