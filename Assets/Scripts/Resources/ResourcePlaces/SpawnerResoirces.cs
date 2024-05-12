using UnityEngine;

public class SpawnerResoircesPlaces : MonoBehaviour
{
    private const string Ground = nameof(Ground);
    private const float MeasuringHeight = 100.0f;
    private const int MaxCountResources = 5;
    private const int MinCountResources = 1;

    [SerializeField] private Transform _container;
    [SerializeField] private ResourcePlace[] _prefabs;
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

        foreach (var prefab in _prefabs)
        {
            for (int i = 0; i < _countRecources; i++)
            {
                Spawn(prefab);
            }
        } 
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

    private void Spawn(ResourcePlace prefab)
    {
        Vector3 position = GetRandomPosition();
        ResourcePlace resourcePlaces = Instantiate(prefab, position, Quaternion.identity);
        resourcePlaces.transform.parent = _container;
    }
}
