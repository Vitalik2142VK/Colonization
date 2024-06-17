using System;
using UnityEngine;

public abstract class ResourceStorage : MonoBehaviour
{
    [SerializeField] private BaseResourceManager _resourceManager;

    private ResourceCounter _counter;

    private int _countResources = 0;
    private int _maxCount = 10;

    public int CountResources => _countResources;
    public bool IsFull => _countResources >= _maxCount;

    private void Awake()
    {
        _counter = GetComponent<ResourceCounter>();
    }

    private void OnEnable()
    {
        _resourceManager.ResourceDelivered += OnAddResource;
    }

    private void Start()
    {
        UpdateView();
    }

    private void OnDisable()
    {
        _resourceManager.ResourceDelivered -= OnAddResource;
    }

    public void SpendResources(int count)
    {
        int result = _countResources - count;

        if (result >= 0)
        {
            _countResources = result;

            UpdateView();
        }
    }

    public void AddCountResources(int count) // fot tests
    {
        int result = _countResources + count;

        if (result <= _maxCount)
            _countResources = result;
        else
            _countResources = _maxCount;

        UpdateView();
    }

    private void UpdateView()
    {
        _counter.UpdateView(_countResources, _maxCount);
    }

    private void OnAddResource(Resource resource)
    {
        if (IsResourceSuitable(resource))
        {
            if (_countResources < _maxCount)
                _countResources++;

            UpdateView();
        }
    }

    public abstract bool IsResourceTypeSuitable(Type typeResource);

    protected abstract bool IsResourceSuitable(IResource resource);
}
