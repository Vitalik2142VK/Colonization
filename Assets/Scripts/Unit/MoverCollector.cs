using System;
using UnityEngine;

public class MoverCollector : MoverUnit
{
    private Resource _foundResource;
    private Rigidbody _foundResourceRigidbody;
    private bool _isResourceFound = false;
    private bool _isResourceTaked = false;

    public event Action<Resource> CanTakeResource;

    public bool IsResourceFound => _isResourceFound;

    public override void Move()
    {
        if (_isResourceFound && _isResourceTaked == false)
        {
            MoveToResource();
        }
        else
        {
            MoveToPoint();

            if (IsThereWaypoint == false)
                PutResource();
        }
    }

    public void SetFoundResource(Resource resource)
    {
        if (resource.TryGetComponent(out Rigidbody rigidbody))
        {
            _foundResource = resource;
            _foundResourceRigidbody = rigidbody;
            _isResourceFound = true;
        } 
    }

    public void PutResource()
    {
        _isResourceFound = false;
        _isResourceTaked = false;
    }

    private void MoveToResource()
    {
        if (_foundResourceRigidbody.isKinematic == false)
        {
            Vector3 resourcePosition = _foundResourceRigidbody.transform.position;
            float radiusReachingPoint = RadiusReachingPoint + _foundResourceRigidbody.transform.localScale.x;

            if (Vector3.Distance(resourcePosition, transform.position) >= radiusReachingPoint)
                transform.position = Vector3.MoveTowards(transform.position, resourcePosition, Speed * Time.deltaTime);
            else
                TakeResource();
        }
        else
        {
            _isResourceFound = false;
        }
    }

    private void TakeResource()
    {
        _isResourceTaked = true;

        EstablishLastPoint();

        CanTakeResource?.Invoke(_foundResource);
    }
}
