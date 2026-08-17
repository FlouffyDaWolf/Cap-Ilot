using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private Animator[] _starAnimators;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _resultClips;
    [SerializeField] private string[] _resultMessages;

    public static ResultsMenu Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _panel.SetActive(false);
    }

    public void ShowResults(int starCount)
    {
        int clampedCount = Mathf.Clamp(starCount, 0, 3);

        _panel.SetActive(true);
        _resultText.text = _resultMessages[clampedCount];

        PlayResultSound(clampedCount);
        PlayStarAnimations(clampedCount);
    }

    public void HideResults()
    {
        _panel.SetActive(false);
    }

    private void PlayResultSound(int starCount)
    {
        _audioSource.clip = _resultClips[starCount];
        _audioSource.Play();
    }

    private void PlayStarAnimations(int starCount)
    {
        for (int i = 0; i < _starAnimators.Length; i++)
        {
            bool isEarned = i < starCount;
            _starAnimators[i].SetTrigger(isEarned ? "Show" : "Idle");
        }
    }
}
