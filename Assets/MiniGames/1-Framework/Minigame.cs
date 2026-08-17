using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public enum State
    {
        Waiting,
        Running,
        Paused,
        Finished
    }

    private State _currentState = State.Waiting;

    public State CurrentState
    {
        get => _currentState;
    }

    public virtual void StartGame()
    {
        if (_currentState == State.Running)
            return;

        _currentState = State.Running;
    }

    public virtual void PauseMinigame()
    {
        if (_currentState != State.Running)
            return;

        _currentState = State.Paused;
    }

    public virtual void ResumeMinigame()
    {
        if (_currentState != State.Paused)
            return;

        _currentState = State.Running;
    }

    public virtual void EndGame()
    {
        if (_currentState == State.Finished)
            return;

        _currentState = State.Finished;
    }
}