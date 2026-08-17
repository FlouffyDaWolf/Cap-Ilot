using System;
using UnityEngine;
using UnityEngine.Events;

public class MinigameEvents : MonoBehaviour
{
    public enum TriggerMode
    {
        Automatic,
        Manual,
        Chance
    }

    [Serializable]
    public class EventData
    {
        [SerializeField] private TriggerMode _triggerMode;
        [SerializeField] private UnityEvent _event;

        [Header("Automatic")]
        [SerializeField] private float _firstDelay = 10f;
        [SerializeField] private float _repeatDelay = 10f;

        [Header("Chance")]
        [Range(0f, 1f)]
        [SerializeField] private float _triggerChance = 0.5f;

        private float _timeRemaining;

        public TriggerMode TriggerMode
        {
            get => _triggerMode;
        }

        public float TriggerChance
        {
            get => _triggerChance;
        }

        public UnityEvent Event
        {
            get => _event;
        }

        public void ResetTimer()
        {
            _timeRemaining = _firstDelay;
        }

        public bool UpdateTimer(float deltaTime)
        {
            _timeRemaining -= deltaTime;

            if (_timeRemaining > 0f)
                return false;

            _timeRemaining = _repeatDelay;

            return true;
        }
    }

    [SerializeField] private EventData _lowFriendshipEvent;
    [SerializeField] private EventData _mediumFriendshipEvent;
    [SerializeField] private EventData _highFriendshipEvent;

    private EventData _currentEvent;
    private bool _isRunning;

    private void Update()
    {
        if (!_isRunning || _currentEvent == null)
            return;

        if (_currentEvent.TriggerMode != TriggerMode.Automatic)
            return;

        if (_currentEvent.UpdateTimer(Time.deltaTime))
            TriggerCurrentEvent();
    }

    public void StartEvents(FriendshipLevel friendshipLevel)
    {
        _currentEvent = GetEvent(friendshipLevel);

        if (_currentEvent == null)
            return;

        _currentEvent.ResetTimer();
        _isRunning = true;
    }

    public void PauseEvents()
    {
        _isRunning = false;
    }

    public void ResumeEvents()
    {
        if (_currentEvent != null)
            _isRunning = true;
    }

    public void StopEvents()
    {
        _isRunning = false;
    }

    public void TryTriggerCurrentEvent()
    {
        if (!_isRunning || _currentEvent == null)
            return;

        switch (_currentEvent.TriggerMode)
        {
            case TriggerMode.Manual:
                TriggerCurrentEvent();
                break;

            case TriggerMode.Chance:
                TryTriggerChanceEvent();
                break;
        }
    }

    private void TryTriggerChanceEvent()
    {
        if (UnityEngine.Random.value > _currentEvent.TriggerChance)
            return;

        TriggerCurrentEvent();
    }

    private void TriggerCurrentEvent()
    {
        _currentEvent.Event?.Invoke();
    }

    private EventData GetEvent(FriendshipLevel friendshipLevel)
    {
        return friendshipLevel switch
        {
            FriendshipLevel.Low => _lowFriendshipEvent,
            FriendshipLevel.Medium => _mediumFriendshipEvent,
            FriendshipLevel.High => _highFriendshipEvent,
            _ => null
        };
    }
}