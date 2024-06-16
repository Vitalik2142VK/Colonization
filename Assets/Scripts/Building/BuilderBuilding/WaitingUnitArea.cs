using System;
using UnityEngine;

public class WaitingUnitArea : MonoBehaviour
{
    private Unit _expectedUnit;

    public event Action UnitEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (UnitEntered == null)
            return;

        if (other.gameObject.TryGetComponent(out Unit unit))
            if (unit == _expectedUnit)
                UnitEntered?.Invoke();
    }

    public void SetExpectedUnit(Unit expectedUnit)
    {
        _expectedUnit = expectedUnit;
    }
}
