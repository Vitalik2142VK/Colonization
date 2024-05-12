using UnityEngine;

public class ResourcesPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Resource _prefab;

    private PoolObjcts<Resource> _pool;

    private void Awake()
    {
        _pool = new PoolObjcts<Resource>(_container, _prefab);
    }

    public Resource GetResource()
    {
        return _pool.GetGameObject();
    }

    public void Put(Resource resource)
    {
        _pool.PutGameObject(resource);
    }
}
