using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneInteractable : Interactable
{
    [SerializeField] GameObject CloneTreePanel;
    public override void Interact()
    {
        CloneTreePanel.SetActive(true);

    }


    //[ContextMenu("yes")]
    //public void StartRun()
    //{
    //    //start generation
    //    //spawn player in the first rooms starting position
    //    GameManager.Instance.LevelManager.AssembleLevel();
    //}
}
