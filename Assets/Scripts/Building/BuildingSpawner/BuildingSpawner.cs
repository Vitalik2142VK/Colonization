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
        flag.SetColor(colorBuilding);

        RemoveViewBuilding();

        Building building = Instantiate(_prefabBuildin);
        building.transform.position = flag.transform.position;
        building.SetColor(colorBuilding);
        building.gameObject.SetActive(false);

        flag.SetBuilding(building);

        return flag;
    }

    public Vector3 GetNewPositionViewBuilding()
    {
        Vector3 position = _previewer.transform.position;

        RemoveViewBuilding();

        return position;
    }

    public void RemoveViewBuilding()
    {
        Destroy(_previewer.gameObject);
    }
}
