using UnityEngine;

public abstract class Border : MonoBehaviour
{
    protected abstract void OnTriggerEnter(Collider other);

    protected abstract void OnTriggerExit(Collider other);
}
