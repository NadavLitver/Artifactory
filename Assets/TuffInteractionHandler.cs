using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuffInteractionHandler : Interactable
{
    [SerializeField] Dialogue m_dialogue;
    GameObject WheelOfFortuneScreen;
    bool interacted;
    private void Start()
    {
        GameManager.Instance.assets.tuffRef = this;
        WheelOfFortuneScreen = GameManager.Instance.assets.wheelOfFortunePrizes.GetFather();
    }
    public override void Interact()
    {
        if(!interacted)
         StartCoroutine(InteractionRoutine());

       

        
    }
    IEnumerator InteractionRoutine()
    {
        interacted = true;
        yield return  GameManager.Instance.dialogueExecuter.Type(m_dialogue);
        WheelOfFortuneScreen.SetActive(true);
    }
}
