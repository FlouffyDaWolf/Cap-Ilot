using System;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    public static TouchInputManager Instance { get; private set; }

    public event Action<Vector2, int> OnTouchStarted;
    public event Action<Vector2, int> OnTouchMoved;
    public event Action<Vector2, int> OnTouchEnded;
    public event Action<Vector2> OnTap;

    [Header("Détection du clic")]
    [SerializeField] private float _maxTapDuration = 0.25f;
    [SerializeField] private float _maxTapMovement = 25f;

    private Vector2 _startPosition;
    private float _startTime;
    private int _trackedFingerId = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouse();
#else
        HandleTouches();
#endif
    }

    private void HandleTouches()
    {
        foreach (Touch touch in Input.touches)
        {
            if (_trackedFingerId == -1 && touch.phase == TouchPhase.Began)
            {
                StartTouch(touch.position, touch.fingerId);
            }

            if (touch.fingerId != _trackedFingerId)
                continue;

            switch (touch.phase)
            {
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    MoveTouch(touch.position, touch.fingerId);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    EndTouch(touch.position, touch.fingerId);
                    break;
            }
        }
    }

    private void HandleMouse()
    {
        const int mouseFingerId = 0;

        if (Input.GetMouseButtonDown(0))
        {
            StartTouch(Input.mousePosition, mouseFingerId);
        }

        if (Input.GetMouseButton(0) &&
            _trackedFingerId == mouseFingerId)
        {
            MoveTouch(Input.mousePosition, mouseFingerId);
        }

        if (Input.GetMouseButtonUp(0) &&
            _trackedFingerId == mouseFingerId)
        {
            EndTouch(Input.mousePosition, mouseFingerId);
        }
    }

    private void StartTouch(Vector2 position, int fingerId)
    {
        _trackedFingerId = fingerId;
        _startPosition = position;
        _startTime = Time.unscaledTime;

        OnTouchStarted?.Invoke(position, fingerId);
    }

    private void MoveTouch(Vector2 position, int fingerId)
    {
        OnTouchMoved?.Invoke(position, fingerId);
    }

    private void EndTouch(Vector2 position, int fingerId)
    {
        OnTouchEnded?.Invoke(position, fingerId);

        float duration = Time.unscaledTime - _startTime;
        float movement = Vector2.Distance(_startPosition, position);

        if (duration <= _maxTapDuration &&
            movement <= _maxTapMovement)
        {
            OnTap?.Invoke(position);
        }

        _trackedFingerId = -1;
    }
}