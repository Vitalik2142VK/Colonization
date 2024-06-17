using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(MouseController))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private MouseController _choicerByMouse;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _choicerByMouse = GetComponent<MouseController>();
    }

    private void Update()
    {
        _playerMover.Move();
        _choicerByMouse.CheckButtonMouse();
        _choicerByMouse.TrackMousePosition();
    }
}
