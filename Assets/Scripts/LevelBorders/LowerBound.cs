using UnityEngine;

public class LowerBound : Border
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMover playerMover))
            playerMover.IsAllowedDown = false;
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMover playerMover))
            playerMover.IsAllowedDown = true;
    }
}
