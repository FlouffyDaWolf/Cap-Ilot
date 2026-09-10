using System;
using UnityEngine;
using UnityEngine.Events;

public class ClickableCollider : MonoBehaviour
{
    [SerializeField] UnityEvent _onClickAction;

    public void OnMouseDown()
    {
        _onClickAction.Invoke();
    }
}
