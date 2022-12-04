using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftinManager : MonoBehaviour
{
    [SerializeField] GameObject craftingPanel;
    [SerializeField] CraftingMap map;
    [SerializeField] InventoryCraftingPanel inventorycraftingPanel;
    [SerializeField] SelectedCraftingPanel selectedCraftingPanel;


    ItemInventory playerInventory => GameManager.Instance.assets.playerActor.PlayerItemInventory;

    public SelectedCraftingPanel SelectedCraftingPanel { get => selectedCraftingPanel; set => selectedCraftingPanel = value; }
    public InventoryCraftingPanel InventorycraftingPanel { get => inventorycraftingPanel; set => inventorycraftingPanel = value; }
    public CraftingMap Map { get => map; set => map = value; }

    public void TurnOnCraftingPanel()
    {
        craftingPanel.SetActive(true);
    }

    public void TurnOffCraftingPanel()
    {
        craftingPanel.SetActive(false);
    }



}
