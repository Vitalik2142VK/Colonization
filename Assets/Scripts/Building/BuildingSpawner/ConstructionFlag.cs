using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ConstructionFlag : MonoBehaviour, IInteractive
{
    private Building _building;
    private Renderer _renderer;

    public Building Building => _building;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetBuilding(Building building)
    {
        if (_building == null)
            _building = building;
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
        _building.transform.position = position;
    }

    public void SetColor(Color color)
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
}
