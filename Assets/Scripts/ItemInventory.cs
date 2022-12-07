using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    [SerializeField] List<ItemType> items = new List<ItemType>();

    public List<ItemType> Items { get => items; }

    public void AddItem(ItemType givenItem)
    {
        items.Add(givenItem);
    }

    public List<RecipeCoponent> GetEachItemByAmount()
    {
        List<RecipeCoponent> itemsByAmount = new List<RecipeCoponent>();
        for (int i = 0; i < Enum.GetNames(typeof(ItemType)).Length; i++)
        {
            if ((ItemType)i == ItemType.Null)
            {
                continue;
            }
            int counter = 0;
            ItemType currentType = (ItemType)i;
            foreach (var item in items)
            {
                if (item == currentType)
                {
                    counter++;
                }
            }
            itemsByAmount.Add(new RecipeCoponent() { amount = counter, itemType = currentType });
        }
        return itemsByAmount;
    }

    public bool IsComponentAvailable(RecipeCoponent comp)
    {
        List<RecipeCoponent> comps = GetEachItemByAmount();
        foreach (var item in comps)
        {
            if (item.itemType == comp.itemType)
            {
                if (comp.amount >= item.amount)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
