using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    private const string Ground = nameof(Ground);

    [SerializeField] private LevelBorders _levelBorders;
    [SerializeField] private BuildingSpawner _buildingSpawner;

    private Camera _camera;
    private Base _selectedBase;

    private bool IsThereSelectedBuilding => _selectedBase != null;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void CheckButtonMouse()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            Select();

        if (Mouse.current.rightButton.wasPressedThisFrame)
            UnSelect();
    }

    public void TrackMousePosition()
    {
        if (IsThereSelectedBuilding == false)
            return;

        if (TryGetRaycastsMousePosition(out RaycastHit[] hits))
        {
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Ground))
                {
                    PreviewerBuilding viewBuilding = _buildingSpawner.GetViewBuilding();
                    viewBuilding.transform.position = _levelBorders.GetCheckedPosition(hit.point);
                }
            }
        }
    }

    private void Select()
    {
        if (IsThereSelectedBuilding)
            SelectPlaceNewBuilding();
        else
            SelectBuilding();
    }

    private void SelectPlaceNewBuilding()
    {
        PreviewerBuilding viewBuilding = _buildingSpawner.GetViewBuilding();

        if (viewBuilding.IsItPossibleBuild)
        {
            if (_selectedBase.CurrentState == Base.State.BuildNewBase)
            {
                Vector3 position = _buildingSpawner.GetNewPositionViewBuilding();
                _selectedBase.ChangePositionNewBase(position);
            }
            else
            {
                ConstructionFlag flag = _buildingSpawner.EstablishBuildingFlag(RandomerColor.GetRandomColor());
                _selectedBase.FixPositionNewBase(flag);
            }
            
            UnSelect();
        }
    }

    private void SelectBuilding()
    {
        if (TryGetRaycastsMousePosition(out RaycastHit[] hits) == false)
            return;

        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out Base selectedBase))
                _selectedBase = selectedBase;
        }
    }

    private bool TryGetRaycastsMousePosition(out RaycastHit[] hits)
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        hits = Physics.RaycastAll(ray);

        return hits.Length > 0;
    }

    private void UnSelect()
    {
        if (IsThereSelectedBuilding)
        {
            _buildingSpawner.RemoveViewBuilding();
            _selectedBase = null;
        }    
    }
}
