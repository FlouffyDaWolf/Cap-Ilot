using UnityEngine;

public class FloatingJoystick : MonoBehaviour
{
    public Vector2 Direction { get; private set; }

    [SerializeField] private float _joystickRadius = 120f;
    [SerializeField] private float _activationDistance = 12f;

    private Vector2 _origin;
    private int _activeFingerId = -1;
    private bool _joystickActivated;

    private void Start()
    {
        TouchInputManager.Instance.OnTouchStarted += HandleTouchStarted;
        TouchInputManager.Instance.OnTouchMoved += HandleTouchMoved;
        TouchInputManager.Instance.OnTouchEnded += HandleTouchEnded;
    }

    private void OnDestroy()
    {
        if (TouchInputManager.Instance == null)
            return;

        TouchInputManager.Instance.OnTouchStarted -= HandleTouchStarted;
        TouchInputManager.Instance.OnTouchMoved -= HandleTouchMoved;
        TouchInputManager.Instance.OnTouchEnded -= HandleTouchEnded;
    }

    private void HandleTouchStarted(Vector2 screenPosition, int fingerId)
    {
        _origin = screenPosition;
        _activeFingerId = fingerId;
        _joystickActivated = false;
        Direction = Vector2.zero;
    }

    private void HandleTouchMoved(Vector2 screenPosition, int fingerId)
    {
        if (fingerId != _activeFingerId)
            return;

        Vector2 difference = screenPosition - _origin;

        if (!_joystickActivated && difference.magnitude >= _activationDistance)
            _joystickActivated = true;

        if (!_joystickActivated)
        {
            Direction = Vector2.zero;
            return;
        }

        Direction = Vector2.ClampMagnitude(
            difference / _joystickRadius,
            1f
        );
    }

    private void HandleTouchEnded(Vector2 screenPosition, int fingerId)
    {
        if (fingerId != _activeFingerId)
            return;

        Direction = Vector2.zero;
        _joystickActivated = false;
        _activeFingerId = -1;
    }
}