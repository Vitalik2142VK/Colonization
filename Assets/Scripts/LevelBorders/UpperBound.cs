using UnityEngine;

public class UpperBound : Border
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.IsAllowedUp = false;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.IsAllowedUp = true;
        }
    }
}