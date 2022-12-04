using System.Collections.Generic;
using UnityEngine;

public class SelectedCraftingPanel : ItemPanel
{
    List<ItemUiSlot> slots = new List<ItemUiSlot>();
    [SerializeField] List<SelectedSlot> selectedSlots = new List<SelectedSlot>();
    public override void SubscribeSlots()
    {
        //subscribe all items to remove from panel
    }



    public void AddToPanel(ItemUiSlot givenSlot)
    {
        if (createdSlots.Count >= 3)
        {
            return;
        }

        //create slot in the appropriate position
        //check which slot is empty 
        slots.Add(givenSlot);
        foreach (var selectedSlot in selectedSlots)
        {
            if (!selectedSlot.Occupied)
            {
                selectedSlot.slot.SetUpSlot(givenSlot);
            }
        }
    }
}

[System.Serializable]
public class SelectedSlot
{
    public ItemUiSlot slot;
    public bool Occupied;
}
