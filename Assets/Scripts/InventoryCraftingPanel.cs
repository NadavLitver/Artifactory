using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCraftingPanel : ItemPanel
{
    //the panel streched along the right side of the screen. 
    //showing the current inventory status with quantities.
    public List<ItemUiSlot> CreatedSlotsGetter { get => createdSlots; }
    public List<Button> createdSlotsButtons;
    public override void SubscribeSlots()
    {
        foreach (var item in createdSlots)
        {
            item.OnSelected.AddListener(GameManager.Instance.CraftingManager.SelectInventoryItem);
            createdSlotsButtons.Add(item.GetComponent<Button>());
        }
    }   




}
