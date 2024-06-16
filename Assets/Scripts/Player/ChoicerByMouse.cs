using UnityEngine;
using UnityEngine.InputSystem;

public class ChoicerByMouse : MonoBehaviour
{
    private const string Ground = nameof(Ground);

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
                    PreviewerBuilding viewBuilding = _selectedBase.GetViewBuilding();
                    viewBuilding.transform.position = hit.point;
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
        PreviewerBuilding viewBuilding = _selectedBase.GetViewBuilding();

        if (viewBuilding.IsItPossibleBuild)
        {
            _selectedBase.FixPositionNewBase();
            _selectedBase = null;
        }
    }

    private void SelectBuilding()
    {
        if (TryGetRaycastsMousePosition(out RaycastHit[] hits) == false)
            return;

        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out Base building))
            {
                if (building.CurrentState == Base.State.Default)
                    _selectedBase = building;

                return;
            }
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
            _selectedBase.RemoveViewBuilding();
            _selectedBase = null;
        }    
    }
}
