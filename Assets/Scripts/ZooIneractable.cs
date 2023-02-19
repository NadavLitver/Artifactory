using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZooIneractable : Interactable
{
    bool firstInteraction;
    public override void Interact()
    {
        if (!firstInteraction)
        {
            GameManager.Instance.assets.baseTutorialHandler.gameObject.SetActive(true);
            firstInteraction = true;
        }
        GameManager.Instance.Zoo.ZooPanel.SetActive(true);
    }
}
