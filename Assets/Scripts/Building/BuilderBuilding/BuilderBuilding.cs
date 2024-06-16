using UnityEngine;

public class BuilderBuilding : MonoBehaviour
{
    [SerializeField] private Building _prefabBuildin;
    [SerializeField] private PreviewerBuilding _previewerPrefab;
    [SerializeField] private ConstructionFlag _prefabFlag;

    private ConstructionFlag _currentFlag;
    private PreviewerBuilding _previewer;

    public PreviewerBuilding GetViewBuilding()
    {
        if (_previewer == null)
            _previewer = Instantiate(_previewerPrefab);

        return _previewer;
    }

    public void EstablishBuildingFlag(Color colorBuilding)
    {
        _currentFlag = Instantiate(_prefabFlag);
        _currentFlag.transform.position = _previewer.transform.position;
        _currentFlag.SetPrefabBuilding(_prefabBuildin);
        _currentFlag.SetColorFlag(colorBuilding);

        RemoveViewBuilding();
    }

    public void RemoveViewBuilding()
    {
        Destroy(_previewer.gameObject);
    }

    public bool TryGetCurrentFlag(out ConstructionFlag flag)
    {
        flag = _currentFlag;

        return _currentFlag != null;
    }
}
