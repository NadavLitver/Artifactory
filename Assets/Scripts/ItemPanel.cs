using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    protected List<ItemUiSlot> createdSlots = new List<ItemUiSlot>();
    //get list of items with amounts, 
    //loop over and create new slots
    public void SetUpSlots(List<RecipeCoponent> components)
    {
        foreach (var comp in components)
        {
            ItemUiSlot newItem = Instantiate(GameManager.Instance.assets.ItemUiSlot, transform);
            newItem.SetUpSlot(comp.itemType);
            newItem.SetUpAmount(comp.amount);
            createdSlots.Add(newItem);
        }
    }


    public void UpdateAmounts(List<RecipeCoponent> components)
    {
        foreach (var comp in components)
        {
            foreach (var itemSlot in createdSlots)
            {
                if (itemSlot.MyItemType == comp.itemType)
                {
                    itemSlot.SetUpAmount(comp.amount);
                }
            }
        }
    }

    public virtual void SubscribeSlots()
    {

    }


}
