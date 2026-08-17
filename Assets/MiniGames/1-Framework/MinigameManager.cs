using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private Friendship _friendship;
    [SerializeField] private Minigame _minigame;
    [SerializeField] private MinigameTimer _timer;
    [SerializeField] private MinigameEvents _events;

    private void Awake()
    {
        if (_minigame == null)
            _minigame = GetComponent<Minigame>();
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        if (_minigame != null)
            _minigame.StartGame();

        if (_timer != null)
            _timer.StartTimer();

        if (_events != null && _friendship != null)
            _events.StartEvents(_friendship.CurrentLevel);
    }

    public void PauseGame()
    {
        if (_minigame == null)
            return;

        _minigame.PauseMinigame();

        if (_timer != null)
            _timer.PauseTimer();

        if (_events != null)
            _events.PauseEvents();
    }

    public void ResumeGame()
    {
        if (_minigame == null)
            return;

        _minigame.ResumeMinigame();

        if (_timer != null)
            _timer.ResumeTimer();

        if (_events != null)
            _events.ResumeEvents();
    }

    public void EndGame()
    {
        if (_minigame == null)
            return;

        _minigame.EndGame();

        if (_timer != null)
            _timer.StopTimer();

        if (_events != null)
            _events.StopEvents();
    }
}