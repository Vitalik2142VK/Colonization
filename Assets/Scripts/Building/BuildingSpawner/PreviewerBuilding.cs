using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PreviewerBuilding : MonoBehaviour
{
    private const float MaxShadeColor = 1.0f;
    private const float MinShadeColor = 0.0f;

    [SerializeField] private InspectorArea _inspectorArea;

    private Renderer _render;
    private Color _colorMaterial;

    public bool IsItPossibleBuild => _inspectorArea.IsZoneEmpty;

    private void Awake()
    {
        _render = GetComponent<Renderer>();
        _colorMaterial = _render.material.color;
    }

    private void OnEnable()
    {
        _inspectorArea.StateZoneChange += OnUpdateColor;
    }

    private void OnDisable()
    {
        _inspectorArea.StateZoneChange -= OnUpdateColor;
    }

    private void OnUpdateColor()
    {
        if (IsItPossibleBuild)
        {
            _colorMaterial.b = MaxShadeColor;
            _colorMaterial.r = MinShadeColor;
        }
        else
        {
            _colorMaterial.b = MinShadeColor;
            _colorMaterial.r = MaxShadeColor;
        }

        _render.material.color = _colorMaterial;

        if (transform.hasChanged)
            ChangeColorChilds();
    }

    private void ChangeColorChilds()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = _colorMaterial;
            }
        }
    }
}
