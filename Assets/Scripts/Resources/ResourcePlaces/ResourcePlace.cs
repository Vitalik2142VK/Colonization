using System;
using System.Collections;
using UnityEngine;

public abstract class ResourcePlace : MonoBehaviour, IResourcePlace
{
    private const float MaxRandomPosition = 0.5f;
    private const float MinRandomPosition = -0.5f;

    private const float WaitRemove = 5.0f;
    private const int MaxCountRecources = 10;
    private const int MinCountRecources = 3;

    private ResourcesPool _pool;
    private int _countRecources = MinCountRecources;

    public void SetResourcesPool(ResourcesPool pool)
    {
        if (_pool == null)
            _pool = pool;
    }

    public void GiveResource()
    {
        if (_countRecources > 0)
        {
            Resource resource = _pool.GetResource();

            Vector3 position = transform.position;
            float spawnHeight = resource.transform.localScale.y + position.y;
            float positionX = position.x + GetRandomValueByAxis();
            float positionZ = position.z + GetRandomValueByAxis();

            resource.gameObject.SetActive(true);
            resource.transform.position = new Vector3(positionX, spawnHeight, positionZ);

            if (--_countRecources == 0)
                StartCoroutine(Remove());
        }
    }

    protected void EstablishCountResources()
    {
        _countRecources = UnityEngine.Random.Range(MinCountRecources, MaxCountRecources);
    }

    protected IEnumerator Remove()
    {
        yield return new WaitForSeconds(WaitRemove);

        Destroy(gameObject);
    }

    private float GetRandomValueByAxis()
    {
        return UnityEngine.Random.Range(MinRandomPosition, MaxRandomPosition);
    }

    public abstract Type GetTypeResource();
}
