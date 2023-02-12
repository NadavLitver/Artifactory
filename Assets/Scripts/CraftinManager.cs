using UnityEngine;

public class CraftinManager : MonoBehaviour
{
    [SerializeField] GameObject craftingPanel;
    [SerializeField] CraftingMap map;
    [SerializeField] InventoryCraftingPanel inventorycraftingPanel;
    [SerializeField] SelectedCraftingPanel selectedCraftingPanel;


    ItemInventory playerInventory => GameManager.Instance.assets.playerActor.PlayerItemInventory;

    public SelectedCraftingPanel SelectedCraftingPanel { get => selectedCraftingPanel;}
    public InventoryCraftingPanel InventorycraftingPanel { get => inventorycraftingPanel;}
    public CraftingMap Map { get => map;}

    public void SelectInventoryItem(ItemUiSlot givenItem)
    {
        if (givenItem.Amount > 0)
        {
            selectedCraftingPanel.AddToPanel(givenItem);
        }
    }

    public void TurnOnCraftingPanel()
    {
        craftingPanel.SetActive(true);
    }

    public void TurnOffCraftingPanel()
    {
        craftingPanel.SetActive(false);
    }

    private void Start()
    {
        playerInventory.OnCraftItem.AddListener(inventorycraftingPanel.UpdateAmounts);
        SetUpCraftingScreens();
    }

    private void SetUpCraftingScreens()
    {
        inventorycraftingPanel.SetUpSlots(playerInventory.GetEachItemByAmount());
        inventorycraftingPanel.SubscribeSlots();
        selectedCraftingPanel.SubscribeSlots();
    }

}
