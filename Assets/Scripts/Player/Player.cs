using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        _playerMover.Move();
    }
}
