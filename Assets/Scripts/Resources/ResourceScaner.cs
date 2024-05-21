using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceScaner : MonoBehaviour
{
    [SerializeField, Min(50.0f)] private float _scanRadius;
    [SerializeField, Min(10.0f)] private float _waitTime;

    public event Action<List<ResourcePlace>> ResourcesFound;

    private List<ResourcePlace> _foundResourcesPlaces;
    private WaitForSeconds _wait;
    private float _waitFirstStart = 2.0f;

    private void Start()
    {
        _foundResourcesPlaces = new List<ResourcePlace>();
        _wait = new WaitForSeconds(_waitTime);

        StartCoroutine(Scan());
    }

    public List<ResourcePlace> GetFoundResources()
    {
        return _foundResourcesPlaces.Where(r => r.enabled).ToList();
    }

    private IEnumerator Scan()
    {
        yield return new WaitForSeconds(_waitFirstStart);

        while (enabled)
        {
            _foundResourcesPlaces.Clear();

            Collider[] scanObjects = Physics.OverlapSphere(transform.position, _scanRadius);

            if (scanObjects.Length != 0)
            {
                foreach (var collider in scanObjects)
                {
                    if (collider.gameObject.TryGetComponent(out ResourcePlace resourcePlace))
                        _foundResourcesPlaces.Add(resourcePlace);
                }

                ResourcesFound?.Invoke(_foundResourcesPlaces);
            }

            yield return _wait;
        }
    }
}