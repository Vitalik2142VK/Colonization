using System;
using System.Linq;
using UnityEngine;

public class CreatorUnit : MonoBehaviour
{
    [SerializeField] private Unit[] _prefabs;
    [SerializeField, Min(0.0f)] private float _spawnHeight;

    public Unit GetUnitTypePrefab(Type typeUnit)
    {
        return _prefabs.Where(u => u.GetType() == typeUnit).First();
    }

    public Unit SpawnByPrefab(Unit prefab)
    {
        float y = transform.position.y + _spawnHeight;
        Unit unit = Instantiate(prefab);
        unit.transform.position = new Vector3(transform.position.x, y, transform.position.z);

        return unit;
    }
}
