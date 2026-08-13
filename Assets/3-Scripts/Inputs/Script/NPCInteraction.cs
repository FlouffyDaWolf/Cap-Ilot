using UnityEngine;

public class NPCInteraction : MonoBehaviour, IClickable
{
    [SerializeField] private string _dialogueText = "Bonjour !";

    public void OnClicked()
    {
        Debug.Log("Dialogue : " + _dialogueText);
    }
}