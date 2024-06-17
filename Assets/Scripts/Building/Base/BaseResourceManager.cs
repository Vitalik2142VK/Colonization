using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BaseResourceManager : MonoBehaviour
{
    [SerializeField] private ResourceStorage[] _resourceStorage;
    [SerializeField] private int _countGiveResources = 0; // for tests
    [SerializeField] private bool _isGiveResources = false; // for tests

    private List<ResourcePlace> _foundResources;

    public event Action<Resource> ResourceDelivered;

    private void Update()
    {
        SetResources();
    }

    public List<ResourcePlace> GetNearestResources(List<ResourcePlace> foundResources)
    {
        _foundResources = foundResources;

        List<ResourcePlace> resources = new List<ResourcePlace>();

        _resourceStorage = _resourceStorage.OrderBy(rs => rs.CountResources).ToArray();

        foreach (var resourceCounter in _resourceStorage)
        {
            if (TryGetNearestResource(out ResourcePlace resource, resourceCounter))
                resources.Add(resource);
        }

        return resources;
    }

    public bool TryGetNearestNeededResource(out ResourcePlace resource)
    {
        int indexNeededResource = 0;

        _resourceStorage = _resourceStorage
            .Where(rs => rs != null && rs.enabled == true)
            .OrderBy(rc => rc.CountResources)
            .ToArray();

        return TryGetNearestResource(out resource, _resourceStorage[indexNeededResource]);
    }

    public void ResourceIsDelivered(Resource resource)
    {
        ResourceDelivered?.Invoke(resource);
        resource.Remove();
    }

    private bool TryGetNearestResource(out ResourcePlace resource, ResourceStorage resourceCounter)
    {
        if (resourceCounter.IsFull == false && _foundResources.Count > 0)
        {
            List<ResourcePlace> placesSameType = _foundResources
                .Where(rp => rp != null && resourceCounter.IsResourceTypeSuitable(rp.GetTypeResource()))
                .ToList();

            if (placesSameType.Count != 0)
            {
                float distance = placesSameType.Min(r => Vector3.Distance(r.transform.position, transform.position));

                resource = placesSameType
                    .Where(r => distance == Vector3.Distance(r.transform.position, transform.position))
                    .First();

                return resource != null;
            }  
        }

        resource = null;

        return false;
    }

    public bool AreEnoughResources(Dictionary<string, int> requiredResources)
    {
        foreach (var resources in requiredResources)
        {
            ResourceStorage resourceStorage = GetResourceStorage(resources);

            if (resourceStorage.CountResources < resources.Value)
                return false;
        }

        return true;
    }

    public void GiveResources(Dictionary<string, int> requiredResources)
    {
        Dictionary<string, ResourceStorage> resourceStorages = new Dictionary<string, ResourceStorage>();

        foreach (var resources in requiredResources)
        {
            ResourceStorage resourceStorage = GetResourceStorage(resources);

            if (resourceStorage.CountResources < resources.Value)
                return;

            resourceStorages.Add(resources.Key, resourceStorage);
        }

        foreach (var resource in requiredResources)
        {
            ResourceStorage resourceStorage = resourceStorages[resource.Key];
            resourceStorage.SpendResources(resource.Value);
        }
    }

    private ResourceStorage GetResourceStorage(KeyValuePair<string, int> resources)
    {
        return _resourceStorage
            .Where(rs => rs.IsResourceTypeSuitable(Type.GetType(resources.Key)))
            .First();
    }

    private void SetResources() // for tests
    {
        if (_isGiveResources == false)
            return;

        foreach (var resource in _resourceStorage)
        {
            resource.AddCountResources(_countGiveResources);
        }

        _isGiveResources = false;
    }
}
