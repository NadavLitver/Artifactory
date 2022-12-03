using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftinManager : MonoBehaviour
{
    [SerializeField] GameObject craftingPanel;
    [SerializeField] CraftingMap map;
    ItemInventory playerInventory => GameManager.Instance.assets.playerActor.PlayerItemInventory;

    public void TurnOnCraftingPanel()
    {
        craftingPanel.SetActive(true);
    }

    public void TurnOffCraftingPanel()
    {
        craftingPanel.SetActive(false);
    }



}
