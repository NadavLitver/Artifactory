using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneInteractable : Interactable
{
    public override void Interact()
    {
        
    }


    [ContextMenu("yes")]
    public void StartRun()
    {
        //start generation
        //spawn player in the first rooms starting position
        GameManager.Instance.LevelManager.AssembleLevel();
    }
}
