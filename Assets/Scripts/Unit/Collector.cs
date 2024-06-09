public class Collector : Unit
{
    private MoverCollector _moverCollector;
    private VisionCollector _visualCollector;
    private ResourcePlace _resourcePlace;

    public ResourcePlace ResourcePlace => _resourcePlace;

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

        if (MoverUnit.IsThereWaypoint)
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

    public bool IsResourcePlacedepleted()
    {
        return _resourcePlace == null || _resourcePlace.enabled == false;
    }

    public void PutResource()
    {
        _moverCollector.PutResource();
    }

    private void OnFindResource(Resource resource)
    {
        _moverCollector.SetFoundResource(resource);
    }

    private void OnTakeResource(Resource resource)
    {
        resource.PickUp(transform);
    }
}
