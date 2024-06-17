using UnityEngine;

public class AreaWorkerBuild : MonoBehaviour
{
    [SerializeField] private Worker _worker;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ConstructionFlag flag))
            _worker.BuildBuilding(flag);
    }
}
