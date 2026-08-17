using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Variables ----------------------------------------------------------------------------- //
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

    // --------------------------- Public Variables --------------------------- //
    // Structure to store the dialogue data for the PNJ
    [System.Serializable]
    public struct DialogueData
    {
        [Header("Main Settings")]
        [Tooltip("The text of the dialogue.")]
        [TextArea(3, 5)] public string DialogueText;

        [Header("Choice Settings")]
        [Tooltip("If true, the dialogue will be a choice dialogue")]
        public bool isChoiceDialogue;

        [Space(5)]

        [Tooltip("The list of choices for the dialogue. Only used if isChoiceDialogue is true. No more than 4.")]
        public List<ChoiceDialogueData> choiceDialogues;
    }

    // Structure to store the choice dialogue data for the PNJ
    [System.Serializable]
    public struct ChoiceDialogueData
    {
        [Tooltip("The text of the choice")]
        public string choiceText;
        [Tooltip("The UnityEvent to invoke when the choice is selected. Next Dialogue is on by default")]
        public UnityEvent onDialogueChoice;
    }

    // --------------------------- Private Variables --------------------------- //
    // Singleton instance of the DialogueManager
    private static DialogueManager instance;

    [Header("Unity Objects")]
    // Unity Objects
    [Tooltip("The Dialogue Canvas")] 
        [SerializeField] private Canvas dialogueCanvas;
    [Tooltip("The list that contains the choice buttons")]
        [SerializeField] private List<Button> choiceButtons;
    [Tooltip("The clickable Zone that will trigger the next dialogue when clicked")]
        [SerializeField] private Image nextDialogueButton;

    [Space(5)]

    [Tooltip("The TextMeshProUGUI component for the dialogue text")]
        [SerializeField] private TextMeshProUGUI dialogueText;
    [Tooltip("The Image that contains the dialogue Text")]
        [SerializeField] private Image dialogueTextContainer;

    [Space(5)]
    [Tooltip("The Image component for the PNJ sprite")]
        [SerializeField] private Image pnjSpriteImage;
    [Tooltip("The TextMeshProUGUI component for the PNJ name")]
        [SerializeField] private TextMeshProUGUI pnjNameText;
    [Tooltip("The Image that contains the PNJ name Text")]
        [SerializeField] private Image pnjNameTextContainer;

    [Header("Settings")]
    [Tooltip("Speed at which the text is displayed in the dialogue")]
        [SerializeField] private float textSpeed = 0.05f;
    [Tooltip ("The number of choices buttons")]
        [SerializeField] private int numberOfChoiceButtons = 4;


    // Current dialogue data being displayed
    private int currentDialogueIndex = 0;
    private List<DialogueData> currentDialogueList;
    private DialogueData currentDialogueData;
    Coroutine printTextCoroutine;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Unity Methods ----------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        dialogueCanvas.enabled = false;
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Public Methods ----------------------------------------------------------------------------- //
    // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

    // --------------------------- Getters / Setters --------------------------- //
    static public DialogueManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("DialogueManager instance is null. Make sure there is a DialogueManager in the scene and to call it after the Awake.");
        }

        return instance;
    }

    // --------------------------- Main Methods --------------------------- //
    public void InitNewDialogue(List<DialogueData> currentDialogueList, string pnjName, Sprite pnjSprite, Color dialogueTextColor, Color dialogueBgColor, TMP_FontAsset dialogueTextFont, Color nameTextColor, Color nameBgColor, TMP_FontAsset nameTextFont)
    {
        this.currentDialogueList = currentDialogueList;
        currentDialogueIndex = -1;

        // Set The dialogue UI elements
        dialogueText.color = dialogueTextColor;
        dialogueText.font = dialogueTextFont;
        dialogueTextContainer.color = dialogueBgColor;

        // Set the PNJ UI elements
        pnjSpriteImage.sprite = pnjSprite;
        pnjNameText.text = pnjName;
        pnjNameText.color = nameTextColor;
        pnjNameText.font = nameTextFont;
        pnjNameTextContainer.color = nameBgColor;



        dialogueCanvas.enabled = true;
        NextDialogue();
    }

    public void NextDialogue()
    {
        Debug.Log("NextDialogue called");
        if (printTextCoroutine != null)
        {
            StopCoroutine(printTextCoroutine);
            printTextCoroutine = null;
            EndOfDialogue();
        }
        else
        {
            // Move to the next dialogue
            currentDialogueIndex++;
            if (currentDialogueIndex < currentDialogueList.Count)
            {
                currentDialogueData = currentDialogueList[currentDialogueIndex];

                printTextCoroutine = StartCoroutine(PrintTextCoroutine());

                if(!currentDialogueData.isChoiceDialogue)
                {
                    foreach (Button button in choiceButtons)
                    {
                        button.gameObject.SetActive(false);
                    }
                }
            }
            // If there are no more dialogues, close the dialogue
            else
            {
                CloseDialogue();
            }
        }
    }

    public void CloseDialogue()
    {
        dialogueCanvas.enabled = false;
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Private Methods ----------------------------------------------------------------------------- //
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // Coroutine to print the dialogue text character by character
    private IEnumerator PrintTextCoroutine()
    {
        int currentCharIndex = 0;
        string textToPrint = currentDialogueData.DialogueText;

        dialogueText.text = "";
        while (currentCharIndex < textToPrint.Length)
        {
            dialogueText.text += textToPrint[currentCharIndex];
            currentCharIndex++;
            yield return new WaitForSeconds(textSpeed);
        }

        EndOfDialogue();
        printTextCoroutine = null;
    }

    // Method to handle the end of the dialogue, enabling choice buttons if necessary and showing all text
    private void EndOfDialogue()
    {
        // Disable the screen click if it's a choice dialogue
        if (currentDialogueData.isChoiceDialogue)
        {
            nextDialogueButton.raycastTarget = false;
        }
        else
        {
            nextDialogueButton.raycastTarget = true;
        }


        dialogueText.text = currentDialogueData.DialogueText;

        if(currentDialogueData.isChoiceDialogue)
        {
            for (int i = 0; i < currentDialogueData.choiceDialogues.Count && i < numberOfChoiceButtons; i++)
            {
                int index = i; // Capture the current index for the listener

                choiceButtons[index].gameObject.SetActive(true);
                choiceButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = currentDialogueData.choiceDialogues[index].choiceText;
                choiceButtons[index].onClick.RemoveAllListeners();

                if(currentDialogueData.choiceDialogues[index].onDialogueChoice != null)
                {
                    choiceButtons[index].onClick.AddListener(() => currentDialogueData.choiceDialogues[index].onDialogueChoice.Invoke());
                }
                else                 
                {
                    choiceButtons[index].onClick.AddListener(() => NextDialogue());
                }
            }
        }
    }
}
