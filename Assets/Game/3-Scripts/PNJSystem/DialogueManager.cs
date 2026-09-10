using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public struct Conversation
{
    public List<Dialogue> Dialogues;
    public bool Completed;
}


[Serializable]
public struct Dialogue
{
    public string Text;
    public Option[] Options;
    public bool EndOfConv;
}

[Serializable]
public struct Option
{
    public string Name;
    public int DialogueAnswerIndex;
    public UnityEvent Action;
}

[Serializable]
public struct OptionButton
{
    public Button Button;
    public TMP_Text Text;
}

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue options")]
    [SerializeField] float _dialogueShowSpeed;
    [SerializeField] TMP_Text _pnjNameField;
    [SerializeField] TMP_Text _dialogueField;
    [SerializeField] Button _nextDialogueButton;
    [SerializeField] Button _clickActionButton;
    [SerializeField] OptionButton[] _options = new OptionButton[4];

    [Header("Extra to temp modify")]
    [SerializeField] GameObject _ui;
    [SerializeField] PlayerMovement2D _playerMovement;

    WaitForSeconds _waitForSeconds;
    Conversation _currentConversation;
    int _dialogueIndex = 0;
    Coroutine _showCoroutine;
    UnityEvent _imOnlyOption = null;

    void Start()
    {
        _waitForSeconds = new(_dialogueShowSpeed);
    }

    void ShowDialogue()
    {
        _clickActionButton.interactable = true;
        Dialogue dialogueToShow = _currentConversation.Dialogues[_dialogueIndex];

        if (dialogueToShow.Options.Length == 1 && dialogueToShow.Options[0].Action.GetPersistentEventCount() != 0)
            _imOnlyOption = dialogueToShow.Options[0].Action;

        for (int i = 0; i < _options.Length; i++)
        {
            OptionButton button = _options[i];
            button.Button.gameObject.SetActive(false);
            button.Button.onClick.RemoveAllListeners();

            if (_imOnlyOption != null || i >= dialogueToShow.Options.Length)
                continue;

            Option option = dialogueToShow.Options[i];
            button.Text.text = option.Name;
            if (option.Action.GetPersistentEventCount() == 0)
                button.Button.onClick.AddListener(() => ToDialogue(option.DialogueAnswerIndex));
            else
                button.Button.onClick.AddListener(() => option.Action.Invoke());
        }

        _dialogueField.text = "";
        _showCoroutine = StartCoroutine(ShowDialogueCoroutine(dialogueToShow.Text));
    }

    IEnumerator ShowDialogueCoroutine(string convText)
    {
        int currentCharIndex = 0;

        while (currentCharIndex < convText.Length)
        {
            _dialogueField.text += convText[currentCharIndex];
            currentCharIndex++;
            yield return _waitForSeconds;
        }

        CompleteShowDialogue();
    }

    void CompleteShowDialogue()
    {
        StopCoroutine(_showCoroutine);
        _showCoroutine = null;

        Dialogue dialogueToShow = _currentConversation.Dialogues[_dialogueIndex];

        _dialogueField.text = dialogueToShow.Text;

        if (_imOnlyOption != null)
            return;

        if (dialogueToShow.Options.Length == 0)
            return;

        _clickActionButton.interactable = false;

        for (int i = 0; i < dialogueToShow.Options.Length; i++)
            _options[i].Button.gameObject.SetActive(true);
    }

    public void StartConversation(PNJData pnjData)
    {
        _pnjNameField.text = pnjData.Name;
        _currentConversation = pnjData.Conversations[pnjData.CurrentConversationIndex];
        _dialogueIndex = 0;
        _ui.SetActive(false);
        gameObject.SetActive(true);
        _playerMovement.CanMove = false;
        ShowDialogue();
    }

    public void EndConversation()
    {
        gameObject.SetActive(false);
        _ui.SetActive(true);
        _playerMovement.CanMove = true;
        _currentConversation = default;
    }

    public void NextDialogue()
    {
        _dialogueIndex++;
        ShowDialogue();
    }

    public void ToDialogue(int index)
    {
        _dialogueIndex = index;
        ShowDialogue();
    }

    public void OnDialogueClick()
    {
        if (_clickActionButton.interactable == false)
            return;

        if (_showCoroutine != null)
        {
            CompleteShowDialogue();
            return;
        }

        if (_imOnlyOption != null)
        {
            _imOnlyOption.Invoke();
            _imOnlyOption = null;
            return;
        }

        if (_currentConversation.Dialogues[_dialogueIndex].EndOfConv)
        {
            EndConversation();
            return;
        }

        NextDialogue();
    }
}
