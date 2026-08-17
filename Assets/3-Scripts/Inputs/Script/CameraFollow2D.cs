using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothTime = 0.15f;
    [SerializeField] private Vector2 _offset;

    private Vector3 _velocity;

    private void LateUpdate()
    {
        if (_player == null)
            return;

        Vector3 targetPosition = new Vector3(
            _player.position.x + _offset.x,
            _player.position.y + _offset.y,
            transform.position.z
        );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _velocity,
            _smoothTime
        );
    }
}