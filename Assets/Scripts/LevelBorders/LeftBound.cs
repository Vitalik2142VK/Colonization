using UnityEngine;

public class LeftBound : Border
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.IsAllowedLeft = false;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.IsAllowedLeft = true;
        }
    }
}