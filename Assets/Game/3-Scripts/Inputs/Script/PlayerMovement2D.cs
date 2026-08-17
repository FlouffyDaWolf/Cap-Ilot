using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;

    [Header("Déplacement")]
    [SerializeField] private float _movementSpeed = 5f;

    [Header("Rotation")]
    [SerializeField] private bool _rotateCharacter = true;
    [SerializeField] private float _rotationSpeed = 720f;
    [SerializeField] private float _rotationOffset = -90f;

    private Rigidbody2D _playerRigidbody;
    private Vector2 _movementDirection;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movementDirection = _joystick.Direction;

        if (_rotateCharacter && _movementDirection.sqrMagnitude > 0.001f)
        {
            float targetAngle =
                Mathf.Atan2(_movementDirection.y, _movementDirection.x)
                * Mathf.Rad2Deg
                + _rotationOffset;

            float newAngle = Mathf.MoveTowardsAngle(
                _playerRigidbody.rotation,
                targetAngle,
                _rotationSpeed * Time.deltaTime
            );

            _playerRigidbody.MoveRotation(newAngle);
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition =
            _playerRigidbody.position
            + _movementDirection * _movementSpeed * Time.fixedDeltaTime;

        _playerRigidbody.MovePosition(targetPosition);
    }
}