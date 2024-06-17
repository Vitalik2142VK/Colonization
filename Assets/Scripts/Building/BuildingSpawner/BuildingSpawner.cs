using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private Building _prefabBuildin;
    [SerializeField] private PreviewerBuilding _previewerPrefab;
    [SerializeField] private ConstructionFlag _prefabFlag;

    private PreviewerBuilding _previewer;

    public PreviewerBuilding GetViewBuilding()
    {
        if (_previewer == null)
            _previewer = Instantiate(_previewerPrefab);

        return _previewer;
    }

    public ConstructionFlag EstablishBuildingFlag(Color colorBuilding)
    {
        ConstructionFlag flag = Instantiate(_prefabFlag);
        flag.transform.position = _previewer.transform.position;
        flag.SetPrefabBuilding(_prefabBuildin);
        flag.SetColorFlag(colorBuilding);

        flag.ReadyBuild += OnSpawnBuilding;

        RemoveViewBuilding();

        return flag;
    }

    public void RemoveViewBuilding()
    {
        Destroy(_previewer.gameObject);
    }

    private void OnSpawnBuilding(ConstructionFlag flag)
    {
        Building building = Instantiate(flag.PrefabBuilding);
        building.transform.position = flag.transform.position;
        building.SetColor(flag.GetColorFlag());

        Worker worker = flag.BuilderWorker;

        if (building is Base newBase)
            newBase.AddUnit(worker);

        flag.ReadyBuild -= OnSpawnBuilding;

        flag.Remove();
    }
}
