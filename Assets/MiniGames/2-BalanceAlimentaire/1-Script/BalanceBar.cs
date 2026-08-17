using UnityEngine;

public class BalanceBar : MonoBehaviour
{
    [SerializeField] private Transform _bar;
    [SerializeField] private float _maximumAngle = 15f;
    [SerializeField] private float _rotationSpeed = 5f;

    private float _targetValue;

    private void Update()
    {
        float targetAngle =
            _targetValue * _maximumAngle;

        Quaternion targetRotation =
            Quaternion.Euler(0f, 0f, -targetAngle);

        _bar.localRotation = Quaternion.Lerp(
            _bar.localRotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );
    }

    public void SetValue(float value)
    {
        _targetValue = Mathf.Clamp(value, -1f, 1f);
    }

    public void ResetBalance()
    {
        _targetValue = 0f;
    }
}