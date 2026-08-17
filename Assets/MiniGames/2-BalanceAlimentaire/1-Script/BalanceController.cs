using UnityEngine;

public class BalanceController : MonoBehaviour
{
    [SerializeField] private float _tiltAngle = 15f;
    [SerializeField] private float _tiltSpeed = 5f;
    [SerializeField] private float _returnSpeed = 3f;

    private Camera _mainCamera;
    private bool _isPaused;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_isPaused)
            return;

        if (Input.GetMouseButton(0))
        {
            Vector2 inputPosition =
                _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            float targetAngle =
                inputPosition.x >= 0f
                    ? -_tiltAngle
                    : _tiltAngle;

            RotateTowards(targetAngle, _tiltSpeed);

            return;
        }

        RotateTowards(0f, _returnSpeed);
    }

    public void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;
    }

    private void RotateTowards(float targetAngle, float speed)
    {
        Quaternion targetRotation =
            Quaternion.Euler(0f, 0f, targetAngle);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            speed * Time.deltaTime
        );
    }
}