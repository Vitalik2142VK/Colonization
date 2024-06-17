using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Unit
{
    [SerializeField, Min(0.5f)] private float _miningTime;

    private MoverWorker _moverWorker;
    private VisionWorker _visualWorker;
    private ResourcePlace _resourcePlace;
    private ConstructionFlag _flag;
    private bool _isMining = false;

    private void Awake()
    {
        GetComponents();

        _moverWorker = MoverUnit as MoverWorker;
        _visualWorker = VisionUnit as VisionWorker;
    }

    private void OnEnable()
    {
        _visualWorker.ResourceFound += OnFindResource;
        _moverWorker.CanTakeResource += OnTakeResource;
    }

    private void Update()
    {
        if (IsBusy && _moverWorker.IsResourceFound == false)
            VisionUnit.Look();

        if (MoverUnit.IsThereWaypoint && _isMining == false)
            MoverUnit.Move();
    }

    private void OnDisable()
    {
        _visualWorker.ResourceFound -= OnFindResource;
        _moverWorker.CanTakeResource -= OnTakeResource;
    }

    public void SetResourcePlace(ResourcePlace resourcePlace)
    {
        _resourcePlace = resourcePlace;
    }

    public void PutResource()
    {
        _moverWorker.PutResource();
    }

    public void MineResource(ResourcePlace resourcePlace)
    {
        if (_resourcePlace == null || resourcePlace != _resourcePlace)
            return;

        if (_moverWorker.IsResourceFound == false)
            StartCoroutine(WaitMineResource());
    }

    public void SetConstructionFlag(ConstructionFlag flag)
    {
        _flag = flag;
    }

    public void TakeResourcesBase()
    {
        _moverWorker.TakeResourcesBase();
    }

    public void BuildBuilding(ConstructionFlag flag)
    {
        if (flag != _flag)
            return;

        PutResource();

        Building building = _flag.Building;
        building.gameObject.SetActive(true);

        if (building is Base newBase)
            newBase.AddUnit(this);

        _flag.Remove();
    }

    private void OnFindResource(Resource resource)
    {
        _moverWorker.SetFoundResource(resource);
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
