using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour, IBuilding
{
    public abstract Dictionary<string, int> GetRequiredResources();

    public void SetColor(Color color)
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out Renderer rendererChild))
                {
                    rendererChild.material.color = color;

                    return;
                }
            }
        }

        if (TryGetComponent(out Renderer renderer))
            renderer.material.color = color;
    }
}
