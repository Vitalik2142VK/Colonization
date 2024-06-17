using UnityEngine;

public class ResourceDistributor : MonoBehaviour
{
    [SerializeField] private ResourcePlace _resourcePlace;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Worker collector))
        {
            collector.MineResource(_resourcePlace);                
        }
    }
}
