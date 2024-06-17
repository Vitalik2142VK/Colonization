using UnityEngine;

public class AreaWorkerMining : MonoBehaviour
{
    [SerializeField] private Worker _worker;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ResourcePlace resource))
            _worker.MineResource(resource);
    }
}
