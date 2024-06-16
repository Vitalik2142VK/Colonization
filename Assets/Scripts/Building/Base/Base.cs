using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceScaner), typeof(BaseResourceManager), typeof(BaseUnitsController))]
[RequireComponent(typeof(CreatorUnit), typeof(BuilderBuilding))]
public partial class Base : Building
{
    [SerializeField] private ResourceCollectionArea _resourceCollectionArea;
    [SerializeField, Min(0.0f)] private float _waitTimeAction;

    private ResourceScaner _resouceScaner;
    private BaseResourceManager _resourceManager;
    private BaseUnitsController _unitsController;
    private CreatorUnit _creatorUnit;
    private BuilderBuilding _builderBuilding;
    private WaitForSeconds _wait;

    public State CurrentState { get; private set; }

    private void Awake()
    {
        _resouceScaner = GetComponent<ResourceScaner>();
        _resourceManager = GetComponent<BaseResourceManager>();
        _unitsController = GetComponent<BaseUnitsController>();
        _creatorUnit = GetComponent<CreatorUnit>();
        _builderBuilding = GetComponent<BuilderBuilding>();

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

    public PreviewerBuilding GetViewBuilding()
    {
        return _builderBuilding.GetViewBuilding();
    }

    public void RemoveViewBuilding()
    {
        _builderBuilding.RemoveViewBuilding();
    }

    public void AddUnit(Unit unit)
    {
        _unitsController.AddUnit(unit);
    }

    public void FixPositionNewBase()
    {
        CurrentState = State.BuildNewBase;
        _builderBuilding.EstablishBuildingFlag(ColorManager.GetRandomColor());
    }

    private void OnSendCollectors(List<ResourcePlace> foundResources)
    {
        if (foundResources.Count == 0)
            return;

        List<ResourcePlace> resourcesPlasces = _resourceManager.GetNearestResources(foundResources);
        _unitsController.SendCollectors(resourcesPlasces);
    }

    private void OnResourceDelivered(Collector collector, Resource resource)
    {
        _resourceManager.ResourceIsDelivered(resource);

        if (_resourceManager.TryGetNearestNeededResource(out ResourcePlace resourcePlace))
        {
            _unitsController.SendCollectorByResource(collector, resourcePlace);
        }
    }

    private void CreateUnit()
    {
        Unit prefab = _creatorUnit.GetUnitTypePrefab(typeof(Collector));
        Dictionary<string, int> requiredResources = prefab.GetRequiredResources();

        if (_resourceManager.AreEnoughResources(requiredResources))
        {
            _resourceManager.GiveResources(requiredResources);
            _unitsController.AddUnit(_creatorUnit.SpawnByPrefab(prefab));
        }
    }

    private void SendCollcetorToNewBase()
    {
        if (_builderBuilding.TryGetCurrentFlag(out ConstructionFlag flag))
        {
            Dictionary<string, int> requiredResources = GetRequiredResources();

            if (_resourceManager.AreEnoughResources(requiredResources) == false)
                return;

            if (_unitsController.TrySentToNewBase(out Collector collector, flag.transform))
            {
                _resourceManager.GiveResources(requiredResources);

                flag.ExpectBuilderCollector(collector);

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
