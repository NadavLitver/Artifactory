using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInteractable : Interactable
{
    GameObject EndInteractablePanel => GameManager.Instance.assets.endInteractablePanel;
    [SerializeField] AudioSource m_audioSource;
    public override void Interact()
    {
        EndInteractablePanel.SetActive(true);
        SoundManager.Play(SoundManager.Sound.EndingInteraction, m_audioSource);
    }

  
}
