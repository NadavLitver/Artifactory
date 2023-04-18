using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInventory : MonoBehaviour, ISaveable
{
    [SerializeField] List<ItemType> items = new List<ItemType>();
    public UnityEvent<List<RecipeCoponent>> OnCraftItem;

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

    public int GetItemTypeAmount(ItemType givenItem)
    {
        int counter = 0;
        foreach (var item in items)
        {
            if (item == givenItem)
            {
                counter++;
            }
        }
        return counter;
    }

    public bool IsComponentAvailable(RecipeCoponent comp)
    {
        List<RecipeCoponent> comps = GetEachItemByAmount();
        foreach (var item in comps)
        {
            if (item.itemType == comp.itemType)
            {
                if (comp.amount <= item.amount)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void CraftItem(CraftingRecipe givenRecipe)
    {
        foreach (var comp in givenRecipe.Components)
        {
            items.Remove(comp.itemType);
        }
        OnCraftItem?.Invoke(GetEachItemByAmount());
        if (!ReferenceEquals(givenRecipe.CraftedItem, null))
        {
            givenRecipe.CraftedItem.Obtain();
        }
        GameManager.Instance.saveLoadSystem.Save();
    }


    public object SaveState()
    {
        return
            new MySaveData()
            {
                _Items = items
            };
    }

    public void LoadState(object state)
    {
        var saveData = (MySaveData)state;
        if (saveData._Items != null)
        {
            items = saveData._Items;
            GameManager.Instance.CraftingManager.InventorycraftingPanel.UpdateAmounts(GetEachItemByAmount());
            //OnCraftItem?.Invoke(GetEachItemByAmount());

        }

    }
    [System.Serializable]
    private struct MySaveData
    {
        public List<ItemType> _Items;
    }
}
