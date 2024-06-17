using System;
using UnityEngine;

public class InspectorArea : MonoBehaviour
{   
    private int _countInterferingObjects = 0;

    public event Action StateZoneChange;

    public bool IsZoneEmpty => _countInterferingObjects == 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IInteractive _))
        {
            _countInterferingObjects++;

            StateZoneChange?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IInteractive _))
        {
            _countInterferingObjects--;

            StateZoneChange?.Invoke();
        }
    }
}
