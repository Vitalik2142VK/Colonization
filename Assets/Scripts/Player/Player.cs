using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(ChoicerByMouse))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private ChoicerByMouse _choicerByMouse;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _choicerByMouse = GetComponent<ChoicerByMouse>();
    }

    private void Update()
    {
        _playerMover.Move();
        _choicerByMouse.CheckButtonMouse();
        _choicerByMouse.TrackMousePosition();
    }
}
