using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private const float MinValueKeyOperation = 0.1f;

    [SerializeField, Min(1.0f)] private float _moveSpeed;
    [SerializeField, Min(1.0f)] private float _bostCoefficient;

    private ControllerCameraRTS _controllerCameraRTS;
    private Vector2 _moveDirection;

    public bool IsAllowedUp { get; set; } = true;
    public bool IsAllowedDown { get; set; } = true;
    public bool IsAllowedRight { get; set; } = true;
    public bool IsAllowedLeft { get; set; } = true;

    private void Awake()
    {
        _controllerCameraRTS = new ControllerCameraRTS();
    }

    private void OnEnable()
    {
        _controllerCameraRTS.Enable();
    }

    private void OnDisable()
    {
        _controllerCameraRTS.Disable();
    }

    public void Move()
    {
        _moveDirection = _controllerCameraRTS.Player.MoveKeyboard.ReadValue<Vector2>();

        if (_moveDirection.sqrMagnitude < MinValueKeyOperation)
            return;

        Vector3 offset = GetOffset();

        float boost = _controllerCameraRTS.Player.Boost.ReadValue<float>();

        if (boost > MinValueKeyOperation)
            offset *= _bostCoefficient;

        transform.Translate(offset);
    }

    private Vector3 GetOffset()
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        float moveByX = _moveDirection.x;
        float moveByY = _moveDirection.y;

        if ((IsAllowedUp == false && _moveDirection.y > 0.0f) || (IsAllowedDown == false && _moveDirection.y < 0.0f))
            moveByY = 0.0f;

        if ((IsAllowedRight == false && _moveDirection.x > 0.0f) || (IsAllowedLeft == false && _moveDirection.x < 0.0f))
            moveByX = 0.0f;

        return new Vector3(moveByX, 0.0f, moveByY) * scaledMoveSpeed;
    }
}
