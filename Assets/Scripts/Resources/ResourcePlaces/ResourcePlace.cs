using System.Collections;
using UnityEngine;

public abstract class ResourcePlace : MonoBehaviour, IResourcePlace
{
    private const float WaitRemove = 5.0f;
    private const int MaxCountRecources = 10;
    private const int MinCountRecources = 3;

    [SerializeField] private ResourcesPool _pool;

    private int _countRecources = MinCountRecources;

    protected ResourcesPool Pool => _pool;
    protected int CountRecources => _countRecources;

    public void GiveResource()
    {
        Resource resource = _pool.GetResource();
        Vector3 position = transform.position;
        float spawnHeight = transform.localScale.y + resource.transform.localScale.y + position.y;

        resource.transform.position = new Vector3(position.x, spawnHeight + position.z);

        if (--_countRecources == 0)
            StartCoroutine(Remove());
    }

    protected void EstablishCountResources()
    {
        _countRecources = Random.Range(MinCountRecources, MaxCountRecources);
    }

    protected IEnumerator Remove()
    {
        yield return new WaitForSeconds(WaitRemove);

        Destroy(gameObject);
    }
}
