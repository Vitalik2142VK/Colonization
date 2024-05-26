using UnityEngine;

public class SpawnerResoircesPlaces : MonoBehaviour
{
    private const string Ground = nameof(Ground);
    private const float MeasuringHeight = 100.0f;
    private const int MaxCountResources = 10;
    private const int MinCountResources = 1;

    [SerializeField] private Transform _container;
    [SerializeField] private ResourcePlace[] _prefabs;
    [SerializeField] private ResourcesPool[] _pools;
    [SerializeField] private float _maxValueByX;
    [SerializeField] private float _minValueByX;
    [SerializeField] private float _maxValueByZ;
    [SerializeField] private float _minValueByZ;
    [SerializeField, Range(MinCountResources, MaxCountResources)] private int _countRecources;
    [SerializeField] private bool _isRandomCount = false;

    private void Start()
    {
        if (_isRandomCount)
            _countRecources = Random.Range(MinCountResources, MaxCountResources);

        ResourcesPool pool;

        foreach (var prefab in _prefabs)
        {
            pool = GetSuitablePool(prefab);

            for (int i = 0; i < _countRecources; i++)
            {
                Spawn(prefab, pool);
            }
        } 
    }

    private ResourcesPool GetSuitablePool(ResourcePlace prefab)
    {
        foreach (var pool in _pools)
        {
            if (pool.ExamplePrefab.IsItSameType(prefab))
                return pool;
        }

        throw new System.Exception("There are no suitable pools.");
    }

    private void Spawn(ResourcePlace prefab, ResourcesPool pool)
    {
        Vector3 position = GetRandomPosition();
        ResourcePlace resourcePlaces = Instantiate(prefab, position, Quaternion.identity);
        resourcePlaces.transform.parent = _container;
        resourcePlaces.SetResourcesPool(pool);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 spawnPosition = new Vector3();
        bool isUniquePosiotion = false;

        while (isUniquePosiotion == false)
        {
            if (TryGetPointSpawn(out spawnPosition))
                isUniquePosiotion = true;
        }

        return spawnPosition;
    }

    private bool TryGetPointSpawn(out Vector3 spawnPosition)
    {
        float valueX = Random.Range(_minValueByX, _maxValueByX);
        float valueZ = Random.Range(_minValueByZ, _maxValueByZ);

        Ray ray = new Ray(new Vector3(valueX, MeasuringHeight, valueZ), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Ground))
            {
                spawnPosition = hit.point;

                return true;
            }
        }

        spawnPosition = new Vector3();
        return false;
    }
}
