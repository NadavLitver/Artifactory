using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuffInteractionHandler : Interactable
{
    [SerializeField] Dialogue startingDialogue;
    [SerializeField] Dialogue endingDialogue;
    [SerializeField] Dialogue endingDialogueWithLegs;

    public bool didPlayerGetLegs;
    GameObject WheelOfFortuneScreen;
    bool interacted;
    private void Start()
    {
        GameManager.Instance.assets.tuffRef = this;
        WheelOfFortuneScreen = GameManager.Instance.assets.wheelOfFortunePrizes.GetFather();
    }
    public override void Interact()
    {
        if (!interacted)
            StartCoroutine(InteractionRoutine());
        else 
            StartCoroutine(GameManager.Instance.dialogueExecuter.Type(didPlayerGetLegs ? endingDialogueWithLegs : endingDialogue));

       

        
    }
    IEnumerator InteractionRoutine()
    {
        interacted = true;
        yield return  GameManager.Instance.dialogueExecuter.Type(startingDialogue);
        WheelOfFortuneScreen.SetActive(true);
    }
}
