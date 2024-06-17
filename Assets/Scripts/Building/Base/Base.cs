using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceScaner), typeof(BaseResourceManager), typeof(BaseUnitsController))]
[RequireComponent(typeof(CreatorUnit))]
public partial class Base : Building
{
    [SerializeField] private ResourceCollectionArea _resourceCollectionArea;
    [SerializeField, Min(0.0f)] private float _waitTimeAction;

    private ResourceScaner _resouceScaner;
    private BaseResourceManager _resourceManager;
    private BaseUnitsController _unitsController;
    private CreatorUnit _creatorUnit;
    private ConstructionFlag _flag;
    private WaitForSeconds _wait;

    public State CurrentState { get; private set; }

    private void Awake()
    {
        _resouceScaner = GetComponent<ResourceScaner>();
        _resourceManager = GetComponent<BaseResourceManager>();
        _unitsController = GetComponent<BaseUnitsController>();
        _creatorUnit = GetComponent<CreatorUnit>();

        CurrentState = State.Default;
    }

    private void OnEnable()
    {
        _resouceScaner.ResourcesFound += OnSendCollectors;
        _resourceCollectionArea.ResourceDelivered += OnResourceDelivered;
    }

    private void Start()
    {
        _wait = new WaitForSeconds(_waitTimeAction);

        StartCoroutine(CheckState());
    }

    private void OnDisable()
    {
        _resouceScaner.ResourcesFound -= OnSendCollectors;
        _resourceCollectionArea.ResourceDelivered -= OnResourceDelivered;
    }

    public void AddUnit(Unit unit)
    {
        _unitsController.AddUnit(unit);
    }

    public void FixPositionNewBase(ConstructionFlag flag)
    {
        CurrentState = State.BuildNewBase;
        _flag = flag;
    }

    private void OnSendCollectors(List<ResourcePlace> foundResources)
    {
        if (foundResources.Count == 0)
            return;

        List<ResourcePlace> resourcesPlasces = _resourceManager.GetNearestResources(foundResources);
        _unitsController.SendWorkers(resourcesPlasces);
    }

    private void OnResourceDelivered(Worker collector, Resource resource)
    {
        _resourceManager.ResourceIsDelivered(resource);

        if (_resourceManager.TryGetNearestNeededResource(out ResourcePlace resourcePlace))
        {
            _unitsController.SendCollectorByWorker(collector, resourcePlace);
        }
    }

    private void CreateUnit()
    {
        Unit prefab = _creatorUnit.GetUnitTypePrefab(typeof(Worker));
        Dictionary<string, int> requiredResources = prefab.GetRequiredResources();

        if (_resourceManager.AreEnoughResources(requiredResources))
        {
            _resourceManager.GiveResources(requiredResources);
            _unitsController.AddUnit(_creatorUnit.SpawnByPrefab(prefab));
        }
    }

    private void SendCollcetorToNewBase()
    {
        if (_flag != null && _flag.enabled)
        {
            Dictionary<string, int> requiredResources = GetRequiredResources();

            if (_resourceManager.AreEnoughResources(requiredResources) == false)
                return;

            if (_unitsController.TrySentToNewBase(out Worker collector, _flag.transform))
            {
                _resourceManager.GiveResources(requiredResources);

                _flag.ExpectBuilderCollector(collector);

                CurrentState = State.Default;
            }
        }
        else
        {
            CurrentState = State.Default;
        }  
    }

    private IEnumerator CheckState()
    {
        while (enabled)
        {
            yield return _wait;

            switch (CurrentState)
            {
                case State.BuildNewBase:
                    SendCollcetorToNewBase();
                    break;

                default: 
                    CreateUnit();
                    break;
            }
        }
    }

    public enum State
    {
        Default, BuildNewBase
    }

    public override Dictionary<string, int> GetRequiredResources()
    {
        int countRequiredResources = 5;

        return new Dictionary<string, int>()
        {
            { nameof(HardСoal), countRequiredResources },
            { nameof(Gold), countRequiredResources },
            { nameof(Iron), countRequiredResources }
        };
    }
}
