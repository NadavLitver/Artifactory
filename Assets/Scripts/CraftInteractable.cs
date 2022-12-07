using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInteractable : Interactable
{
    GameObject CraftingPanel => GameManager.Instance.assets.CraftingPanel;
    public override void Interact()
    {
        CraftingPanel.SetActive(true);
    }
}
