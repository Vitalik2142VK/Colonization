using UnityEngine;

public abstract class VisionUnit : MonoBehaviour
{
    [SerializeField] private float _viewingRadius;

    protected float ViewingRadius => _viewingRadius;

    public abstract void Look();
}
