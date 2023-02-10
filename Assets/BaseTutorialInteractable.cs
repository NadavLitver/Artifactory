using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseTutorialInteractable : Interactable
{
    public UnityEvent onInteract;
    public override void Interact()
    {
        onInteract?.Invoke();
    }
}
