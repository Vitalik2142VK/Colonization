using System;
using UnityEngine;

public class ResourceCollectionArea : MonoBehaviour
{
    public event Action<Collector, Resource> ResourceDelivered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Collector collector))
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
