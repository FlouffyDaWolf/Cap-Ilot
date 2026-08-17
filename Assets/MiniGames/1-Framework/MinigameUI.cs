using TMPro;
using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    [SerializeField] private MinigameScore _score;
    [SerializeField] private MinigameTimer _timer;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _timerText;

    private void OnEnable()
    {
        if (_score != null)
        {
            _score.OnScoreChanged += UpdateScore;
            UpdateScore(_score.CurrentScore);
        }

        if (_timer != null)
        {
            _timer.OnTimeChanged += UpdateTimer;
            UpdateTimer(_timer.TimeRemaining);
        }
    }

    private void OnDisable()
    {
        if (_score != null)
            _score.OnScoreChanged -= UpdateScore;

        if (_timer != null)
            _timer.OnTimeChanged -= UpdateTimer;
    }

    private void UpdateScore(int score)
    {
        if (_scoreText == null)
            return;

        _scoreText.text = $"Score : {score}";
    }

    private void UpdateTimer(float timeRemaining)
    {
        if (_timerText == null)
            return;

        _timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }
}