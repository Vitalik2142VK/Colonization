using System;
using UnityEngine;

public class ResourceCollectionArea : MonoBehaviour
{
    public event Action<Worker, Resource> ResourceDelivered;

    private void OnTriggerStay(Collider other)
    {
        GetResource(other);
    }

    private void GetResource(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Worker collector))
        {
            int countChilds = collector.transform.childCount;

            if (countChilds > 0)
            {
                for (int i = 0; i < countChilds; i++)
                {
                    if (collector.transform.GetChild(i).TryGetComponent(out Resource resource))
                        ResourceDelivered?.Invoke(collector, resource);
                }
            }
        }
    }
}
