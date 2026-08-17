using System;
using UnityEngine;

public class MinigameTimer : MonoBehaviour
{
    [SerializeField] private float _duration = 60f;

    private float _timeRemaining;
    private bool _isRunning;

    public float TimeRemaining
    {
        get => _timeRemaining;
    }

    public float Duration
    {
        get => _duration;
    }

    public event Action<float> OnTimeChanged;
    public event Action OnTimeUp;

    private void Update()
    {
        if (!_isRunning)
            return;

        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            _isRunning = false;

            OnTimeChanged?.Invoke(_timeRemaining);
            OnTimeUp?.Invoke();

            return;
        }

        OnTimeChanged?.Invoke(_timeRemaining);
    }

    public void StartTimer()
    {
        _timeRemaining = _duration;
        _isRunning = true;

        OnTimeChanged?.Invoke(_timeRemaining);
    }

    public void PauseTimer()
    {
        _isRunning = false;
    }

    public void ResumeTimer()
    {
        if (_timeRemaining <= 0f)
            return;

        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public void ResetTimer()
    {
        _timeRemaining = _duration;
        _isRunning = false;

        OnTimeChanged?.Invoke(_timeRemaining);
    }
}