using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZooIneractable : Interactable
{
    bool firstInteraction;
    public UnityEvent OnInteracted;
    public override void Interact()
    {
       
        if (!firstInteraction)
        {
            GameManager.Instance.assets.baseTutorialHandler.gameObject.SetActive(true);
            GameManager.Instance.assets.baseTutorialHandler.CallZooTutorial();
            firstInteraction = true;
        }
        OnInteracted?.Invoke();
        GameManager.Instance.Zoo.ZooPanel.SetActive(true);
    }
}
