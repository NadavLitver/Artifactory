using System.Collections;
using UnityEngine;

public class TuffInteractionHandler : Interactable
{
    [SerializeField] Dialogue startingDialogue;
    [SerializeField] Dialogue endingDialogue;
    [SerializeField] Dialogue endingDialogueWithLegs;
    [SerializeField] Collider2D m_collider;
    [SerializeField] AudioSource m_audioSource;
    public bool didPlayerGetLegs;
    GameObject WheelOfFortuneScreen;
    bool interacted;
    private void Start()
    {
        m_collider = GetComponent<Collider2D>();
        GameManager.Instance.assets.tuffRef = this;
        WheelOfFortuneScreen = GameManager.Instance.assets.wheelOfFortunePrizes.GetFather();
    }
    public override void Interact()
    {
        StartCoroutine(InteractionRoutine());
    }
    IEnumerator InteractionRoutine()
    {
        SoundManager.Play(SoundManager.Sound.TuffInteraction, m_audioSource);
        if (interacted)
        {
            m_collider.enabled = false;

            yield return GameManager.Instance.dialogueExecuter.Type(didPlayerGetLegs ? endingDialogueWithLegs : endingDialogue);
            m_collider.enabled = true;

            yield break;
        }
        m_collider.enabled = false;
        interacted = true;
        yield return GameManager.Instance.dialogueExecuter.Type(startingDialogue);
        WheelOfFortuneScreen.SetActive(true);
        m_collider.enabled = true;

    }
    [ContextMenu("Set Interacted false")]
    public void SetInteractedFalse() => interacted = false;
}
