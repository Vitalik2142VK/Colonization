using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Resource : MonoBehaviour, IResource 
{
    private Rigidbody _rigidbody;

    public event Action<Resource> Delete;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform parent, float holdDistance = -1.0f)
    {
        transform.SetParent(parent);
        transform.localPosition = new Vector3(0.0f, 0.0f, holdDistance);

        _rigidbody.isKinematic = true;
    }

    public void Remove()
    {
        _rigidbody.isKinematic = false;

        Delete?.Invoke(this);
    }
}
