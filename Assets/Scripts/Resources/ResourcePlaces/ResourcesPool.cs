using UnityEngine;

public class ResourcesPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Resource _prefab;

    private PoolObjcts<Resource> _pool;

    public Resource ExamplePrefab => _prefab;

    private void Awake()
    {
        _pool = new PoolObjcts<Resource>(_container, _prefab);
    }

    public Resource GetResource()
    {
        Resource resource = _pool.GetGameObject();
        resource.Delete += PutResource;

        return resource;
    }

    public void PutResource(Resource resource)
    {
        _pool.PutGameObject(resource);
        resource.Delete -= PutResource;
    }
}
