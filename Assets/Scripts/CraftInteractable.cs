using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CraftInteractable : Interactable
{
    GameObject CraftingPanel => GameManager.Instance.assets.CraftingPanel;
    public UnityEvent OnInteract;
    public IProximityDetection m_detection;
    public override void Interact()
    {
        CraftingPanel.SetActive(true);
        OnInteract?.Invoke();
    }
}
