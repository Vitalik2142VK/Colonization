using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : Unit
{
    [SerializeField, Min(0.5f)] private float _miningTime;

    private MoverCollector _moverCollector;
    private VisionCollector _visualCollector;
    private ResourcePlace _resourcePlace;
    private bool _isMining = false;

    private void Awake()
    {
        GetComponents();

        _moverCollector = MoverUnit as MoverCollector;
        _visualCollector = VisionUnit as VisionCollector;
    }

    private void OnEnable()
    {
        _visualCollector.ResourceFound += OnFindResource;
        _moverCollector.CanTakeResource += OnTakeResource;
    }

    private void Update()
    {
        if (IsBusy && _moverCollector.IsResourceFound == false)
            VisionUnit.Look();

        if (MoverUnit.IsThereWaypoint && _isMining == false)
            MoverUnit.Move();
    }

    private void OnDisable()
    {
        _visualCollector.ResourceFound -= OnFindResource;
        _moverCollector.CanTakeResource -= OnTakeResource;
    }

    public void SetResourcePlace(ResourcePlace resourcePlace)
    {
        _resourcePlace = resourcePlace;
    }

    public void PutResource()
    {
        _moverCollector.PutResource();
    }

    public void MineResource(ResourcePlace resourcePlace)
    {
        if (_resourcePlace == null || resourcePlace != _resourcePlace)
            return;

        if (_moverCollector.IsResourceFound == false)
            StartCoroutine(WaitMineResource());
    }

    public void TakeResourcesBase()
    {
        _moverCollector.TakeResourcesBase();
    }

    private void OnFindResource(Resource resource)
    {
        _moverCollector.SetFoundResource(resource);
    }

    private void OnTakeResource(Resource resource)
    {
        resource.PickUp(transform);
    }

    private IEnumerator WaitMineResource()
    {
        _isMining = true;

        yield return new WaitForSeconds(_miningTime);

        _resourcePlace.GiveResource();
        _isMining = false;
    }

    public override Dictionary<string, int> GetRequiredResources()
    {
        int countRequiredResources = 3;

        return new Dictionary<string, int>()
        {
            { nameof(Hard—oal), countRequiredResources },
            { nameof(Gold), countRequiredResources },
            { nameof(Iron), countRequiredResources }
        };
    }
}
