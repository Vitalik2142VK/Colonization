using System;
using UnityEngine;

public class MoverCollector : MoverUnit
{
    public event Action<Resource> CanTakeResource;

    private Resource _foundResource;
    private bool _isResourceFinded = false;
    private bool _isResourceTaked = false;

    public void SetFoundResource(Resource resource)
    {
        _foundResource = resource;
        _isResourceFinded = true;
    }

    public override void Move()
    {
        //if (_isResourceFinded && _isResourceTaked == false)
        //    MoveToResource();
        //else
        //    MoveToPoint();

        MoveToPoint();
    }

    public void PutResource()
    {
        _isResourceFinded = false;
        _isResourceTaked = false;
    }

    private void MoveToResource()
    {
        Vector3 resourcePosition = _foundResource.transform.position;
        float radiusReachingPoint = RadiusReachingPoint + _foundResource.transform.localScale.x;

        if (Vector3.Distance(resourcePosition, transform.position) <= radiusReachingPoint)
            transform.position = Vector3.MoveTowards(transform.position, resourcePosition, Speed * Time.deltaTime);
        else
            TakeResource();
    }

    private void TakeResource()
    {
        CanTakeResource?.Invoke(_foundResource);

        _isResourceTaked = true;
    }
}
