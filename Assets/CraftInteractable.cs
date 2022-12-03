using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInteractable : Interactable
{
    [SerializeField] GameObject CraftingPanel;
    public override void Interact()
    {
        CraftingPanel.SetActive(true);
    }
}
