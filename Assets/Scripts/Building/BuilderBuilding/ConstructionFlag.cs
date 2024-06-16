using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ConstructionFlag : MonoBehaviour
{
    [SerializeField] private WaitingUnitArea _waitingUnitArea;

    private Building _prefabBuilding;
    private Collector _builderCollector;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _waitingUnitArea.UnitEntered += OnBuild;
    }

    private void OnDisable()
    {
        _waitingUnitArea.UnitEntered -= OnBuild;
    }

    public void SetPrefabBuilding(Building prefabBuilding)
    {
        _prefabBuilding = prefabBuilding;
    }

    public void ExpectBuilderCollector(Collector builderCollector)
    {
        _builderCollector = builderCollector;
        _waitingUnitArea.SetExpectedUnit(_builderCollector);
    }

    public void SetColorFlag(Color color)
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out Renderer renderer))
                {
                    renderer.material.color = color;

                    return;
                }
            }
        }

        _renderer.material.color = color;
    }

    private void OnBuild()
    {
        Building building = Instantiate(_prefabBuilding);
        building.transform.position = transform.position;
        SetColorBuilding(building);

        if (building is Base newBase)
            newBase.AddUnit(_builderCollector);

        _builderCollector.PutResource();

        Destroy(gameObject);
    }

    private void SetColorBuilding(Building building)
    {
        Color color = _renderer.material.color;

        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out Renderer rendererChild))
                {
                    color = rendererChild.material.color;

                    break;
                }
            }
        }

        if (building.transform.childCount > 0)
        {
            foreach (Transform child in building.transform)
            {
                if (child.TryGetComponent(out Renderer rendererChild))
                {
                    rendererChild.material.color = color;

                    return;
                }
            }
        }

        if (building.TryGetComponent(out Renderer renderer))
            renderer.material.color = color;
    }
}
