using UnityEngine;

public class RightBound : Border
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.IsAllowedRight = false;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.IsAllowedRight = true;
        }
    }
}
