using System;
using UnityEngine;

public class MoverCollector : MoverUnit
{
    public event Action<Resource> CanTakeResource;

    private Resource _foundResource;
    private bool _isResourceFound = false;
    private bool _isResourceTaked = false;

    public bool IsResourceFound => _isResourceFound;

    public void SetFoundResource(Resource resource)
    {
        _foundResource = resource;
        _isResourceFound = true;
    }

    public override void Move()
    {
        if (_isResourceFound && _isResourceTaked == false)
            MoveToResource();
        else
        {
            MoveToPoint();

            if (IsThereWaypoint == false)
                PutResource();
        }
    }

    private void PutResource()
    {
        _isResourceFound = false;
        _isResourceTaked = false;
    }

    private void MoveToResource()
    {
        if (_foundResource.GetComponent<Rigidbody>().isKinematic == false)
        {
            Vector3 resourcePosition = _foundResource.transform.position;
            float radiusReachingPoint = RadiusReachingPoint + _foundResource.transform.localScale.x;

            if (Vector3.Distance(resourcePosition, transform.position) >= radiusReachingPoint)
                transform.position = Vector3.MoveTowards(transform.position, resourcePosition, Speed * Time.deltaTime);
            else
                TakeResource();
        }
        else
            _isResourceFound = false;
    }

    private void TakeResource()
    {
        _isResourceTaked = true;

        EstablishLastPoint();

        CanTakeResource?.Invoke(_foundResource); 
    }
}
