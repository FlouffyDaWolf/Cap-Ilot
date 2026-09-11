using UnityEngine;
using UnityEngine.EventSystems;

public class PNJ : MonoBehaviour
{
    [SerializeField] PNJData _data;
    [SerializeField] DialogueManager _dialogueManager;

    bool _inArea = false;

    public void Talk()
    {
        if (_inArea)
            _dialogueManager.StartConversation(_data);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player"))
            return;
        
        _inArea = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player"))
            return;

        _inArea = false;
    }
}
