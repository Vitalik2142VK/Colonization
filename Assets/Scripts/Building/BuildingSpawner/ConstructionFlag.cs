using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ConstructionFlag : MonoBehaviour
{
    [SerializeField] private WaitingUnitArea _waitingUnitArea;

    private Building _prefabBuilding;
    private Worker _builderWorker;
    private Renderer _renderer;

    public event Action<ConstructionFlag> ReadyBuild;

    public Building PrefabBuilding => _prefabBuilding;
    public Worker BuilderWorker => _builderWorker;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _waitingUnitArea.UnitEntered += OnGetReadyBuild;
    }

    private void OnDisable()
    {
        _waitingUnitArea.UnitEntered -= OnGetReadyBuild;
    }

    public void SetPrefabBuilding(Building prefabBuilding)
    {
        if (_prefabBuilding == null)
            _prefabBuilding = prefabBuilding;
    }

    public void ExpectBuilderCollector(Worker builderCollector)
    {
        _builderWorker = builderCollector;
        _waitingUnitArea.SetExpectedUnit(_builderWorker);
    }

    public Color GetColorFlag()
    {
        Color color = _renderer.material.color;

        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out Renderer rendererChild))
                    color = rendererChild.material.color;
            }
        }

        return color;
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

    public void Remove()
    {
        Destroy(gameObject);
    }

    private void OnGetReadyBuild()
    {
        ReadyBuild?.Invoke(this);

        _builderWorker.PutResource();
    }
}
