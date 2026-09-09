using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using TMPro;

public class PNJ : MonoBehaviour
{
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Variables ----------------------------------------------------------------------------- //
    // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

    // --------------------------- Public Variables --------------------------- //

    // --------------------------- Private Variables --------------------------- //
    [Header("Main Settings")]

    [Tooltip("The name of the PNJ.")]
        [SerializeField] private string pnjName;
    [Tooltip("The sprite of the PNJ.")]
        [SerializeField] private Sprite pnjSprite;

    [Space(5)]

    [Tooltip("Launch the dialogue on start")]
        [SerializeField] private bool launchDialogueOnStart = false;

    [Tooltip("gameObjects to active when the player is near")]
        [SerializeField] List<GameObject> gameObjectsToActive;

    [Header("Dialogue Section")]
    // Dialogue Section
    private DialogueManager dialogueManager;
    [Tooltip("The Dialogue Canvas Prefab in case it's not instanciate")] // Don't rely on this, it's better to have a DialogueManager in the scene.
        [SerializeField] private GameObject dialogueCanvasPrefab;

    [Space(10)]
    [Tooltip("The color of the dialogue Text")]
        [SerializeField] private Color textColor = Color.black;
    [Tooltip("The sprite of the dialogueTextBox")]
        [SerializeField] private Sprite textBackgroundSprite;
    [Tooltip("The color of the dialogue Text Background")]
        [SerializeField] private Color textBackgroundColor = Color.white;
    [Tooltip("The font of the dialogue Text")]
        [SerializeField] private TMP_FontAsset textFont;

    [Space(5)]
    [Tooltip("The color of the name Text")]
        [SerializeField] private Color nameTextColor = Color.black;
    [Tooltip("The sprite of the nameTextBox")]
    [SerializeField] private Sprite nameTextBackgroundSprite;
    [Tooltip("The color of the name Text Background")]
        [SerializeField] private Color nameTextBackgroundColor = Color.white;
    [Tooltip("The font of the name Text")]
        [SerializeField] private TMP_FontAsset nameTextFont;

    [Space(10)]

    [Tooltip("The list of dialogues for the PNJ.")]
        [SerializeField] private List<DialogueManager.DialogueData> dialogues = new List<DialogueManager.DialogueData>();







    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Unity Methods ----------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    private void Start()
    {
        dialogueManager = DialogueManager.GetInstance();
        if (dialogueManager == null)
        {
            Debug.LogError("No DialogueManager has been found. A new one will be instantiated.");
            Instantiate(dialogueCanvasPrefab);

            dialogueManager = DialogueManager.GetInstance();
            if (dialogueManager == null)
            {
                Debug.LogError("Failed to instantiate DialogueManager from the prefab.");
            }
        }

        if (launchDialogueOnStart)
        {
            DialogueStart();
        }

        foreach (GameObject obj in gameObjectsToActive)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject obj in gameObjectsToActive)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject obj in gameObjectsToActive)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    // ----------------------------------------------------------------------------- Public Methods ----------------------------------------------------------------------------- //
    // -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- //

    // --------------------------- Getters / Setters --------------------------- //
    public string GetPNJName() 
    { 
        return pnjName; 
    }

    // --------------------------- Main Methods --------------------------- //
    public virtual void CustomAction()
    {
        Debug.LogError("CustomAction() method is not implemented in the PNJ class. Please override this method in a derived class.");
    }
    public void DialogueStart()
    {
        dialogueManager.InitNewDialogue(dialogues, pnjName, pnjSprite, textColor, textBackgroundSprite, textBackgroundColor, textFont, nameTextColor, nameTextBackgroundSprite, nameTextBackgroundColor, nameTextFont);
    }
}
