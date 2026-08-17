using System;
using UnityEngine;

public class MinigameScore : MonoBehaviour
{
    [SerializeField] private int _startingScore = 0;

    private int _currentScore;
    public int CurrentScore { get => _currentScore; }


    public event Action<int> OnScoreChanged;


    private void Awake()
    {
        ResetScore();
    }

    public void AddScore(int amount = 1)
    {
        SetScore(_currentScore + amount);
    }

    public void RemoveScore(int amount = 1)
    {
        SetScore(_currentScore - amount);
    }

    public void SetScore(int value)
    {
        _currentScore = value;
        OnScoreChanged?.Invoke(_currentScore);
    }

    public void ResetScore()
    {
        SetScore(_startingScore);
    }
}