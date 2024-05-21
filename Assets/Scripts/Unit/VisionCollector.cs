using System;
using UnityEngine;

public class VisionCollector : VisionUnit
{
    [SerializeField, Min(1.0f)] private float _timeWait;

    public event Action<Resource> ResourceFound;

    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(_timeWait);
    }

    public override void Look()
    {
        if (_timer.IsTimeUp)
        {
            FindResource();

            _timer.UpdateWaitingTime();
        }

        _timer.MakeCountdown();
    }

    private bool TryFindResource(out Resource resource)
    {
        foreach (var collider in Physics.OverlapSphere(transform.position, ViewingRadius))
        {
            if (collider.TryGetComponent(out resource))
                if (resource.GetComponent<Rigidbody>().isKinematic == false)
                    return true;
        }

        resource = null;

        return false;
    }

    private void FindResource()
    {
        if (TryFindResource(out Resource resource))
            ResourceFound?.Invoke(resource);
    }
}