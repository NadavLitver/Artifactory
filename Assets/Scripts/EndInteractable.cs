using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInteractable : Interactable
{
    GameObject EndInteractablePanel => GameManager.Instance.assets.endInteractablePanel;
    public override void Interact()
    {
        EndInteractablePanel.SetActive(true);
    }

  
}
