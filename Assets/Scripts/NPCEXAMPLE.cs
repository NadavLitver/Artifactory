using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEXAMPLE : Interactable
{
    [SerializeField] Dialogue m_dialogue;
    public override void Interact()
    {
        
        GameManager.Instance.dialogueExecuter.Print(m_dialogue);
    }
}
