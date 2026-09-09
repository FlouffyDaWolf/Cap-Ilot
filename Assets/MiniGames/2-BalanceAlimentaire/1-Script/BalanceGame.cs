using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BalanceGame : Minigame
{
    [SerializeField] private MinigameScore _score;
    [SerializeField] private MinigameEvents _events;
    [SerializeField] private FoodSpawner _foodSpawner;
    [SerializeField] private SortingZoneSwitcher _zoneSwitcher;
    [SerializeField] private BalanceController[] _balanceControllers;

    public override void StartGame()
    {
        base.StartGame();

        _score?.ResetScore();
        _zoneSwitcher?.ResetZones();
        _foodSpawner?.StartSpawning();

        SetBalancesPaused(false);
    }

    public override void PauseMinigame()
    {
        if (CurrentState != State.Running)
            return;

        base.PauseMinigame();

        _foodSpawner?.PauseSpawning();

        SetBalancesPaused(true);
    }

    public override void ResumeMinigame()
    {
        if (CurrentState != State.Paused)
            return;

        base.ResumeMinigame();

        _foodSpawner?.ResumeSpawning();

        SetBalancesPaused(false);
    }

    public override void EndGame()
    {
        if (CurrentState == State.Finished)
            return;

        base.EndGame();

        _foodSpawner?.StopSpawning();

        SetBalancesPaused(true);
    }

    public void HandleFoodEnteredZone(
        Food food,
        SortingZone sortingZone
    )
    {
        if (CurrentState != State.Running)
            return;

        bool isCorrect =
            food.Data.Category == sortingZone.AcceptedCategory;

        if (isCorrect)
        {
            HandleCorrectFood(food);
            return;
        }

        HandleIncorrectFood(food);
    }

    public void TriggerZoneShuffle()
    {
        _zoneSwitcher?.ShuffleZones();
    }

    public void TriggerFoodBurst()
    {
        _foodSpawner?.SpawnBurst(Random.Range(5, 11));
    }

    private void HandleCorrectFood(Food food)
    {
        _score?.AddScore();

        _foodSpawner?.ReleaseFood(food);
    }

    private void HandleIncorrectFood(Food food)
    {
        bool wasHelped =
            _events != null &&
            _events.TryTriggerCurrentEvent();

        if (!wasHelped)
            _score?.RemoveScore();

        _foodSpawner?.ReleaseFood(food);
    }

    private void SetBalancesPaused(bool isPaused)
    {
        foreach (BalanceController balanceController in _balanceControllers)
        {
            if (balanceController != null)
                balanceController.SetPaused(isPaused);
        }
    }
}