using System.Collections.Generic;
using UnityEngine;

public class SelectedCraftingPanel : ItemPanel
{
    //panel at the bottom right corner of the screen. shows the 3 currently selected items in order
    [SerializeField] List<SelectedSlot> selectedSlots = new List<SelectedSlot>();
    public override void SubscribeSlots()
    {
        foreach (var item in selectedSlots)
        {
            item.slot.ResetSlot();
            item.slot.OnSelected.AddListener(RemoveFromPanel);
        }
    }


    public void AddToPanel(ItemUiSlot givenSlot)
    {//sets up the next free slot
        Debug.Log("selecting item");
        foreach (var selectedSlot in selectedSlots)
        {
            if (selectedSlot.slot.MyItemType == givenSlot.MyItemType)
            {
                return;
            }
        }
        //create slot in the appropriate position
        //check which slot is empty 
        foreach (var selectedSlot in selectedSlots)
        {
            if (!selectedSlot.Occupied)
            {
                selectedSlot.slot.SetUpSlot(givenSlot, Color.white);
                selectedSlot.Occupied = true;
                UpdateCraftingMap();
                //send itemtype and index, loop over crafting map, unlock all lines that match up to the items selected
                break;
            }
        }
    }

    private void UpdateCraftingMap()
    {
        List<ItemType> itemsByOrder = new List<ItemType>();
        foreach (var item in selectedSlots)
        {
            itemsByOrder.Add(item.slot.MyItemType);
        }
        GameManager.Instance.CraftingManager.Map.UpdateActivatedLines(itemsByOrder);
    }

    public void RemoveFromPanel(ItemUiSlot givenSlot)
    {
        //sub on selected to this
        foreach (var item in selectedSlots)
        {
            if (item.slot.MyItemType == givenSlot.MyItemType)
            {
                item.slot.ResetSlot();
                item.Occupied = false;
                break;
            }
        }
        UpdateCraftingMap();
    }

    public void ClearPanel()
    {
        foreach (var item in selectedSlots)
        {
            item.slot.ResetSlot();
            item.Occupied = false;
        }
    }
}



[System.Serializable]
public class SelectedSlot
{
    public ItemUiSlot slot;
    public bool Occupied;
}
