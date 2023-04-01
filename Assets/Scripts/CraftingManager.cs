using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] GameObject craftingPanel;
    [SerializeField] CraftingMap relicCraftinMap;
    [SerializeField] CraftingMap weaponCraftingMap;
    [SerializeField] GameObject weaponMap;
    [SerializeField] GameObject relicMap;
    CraftingMap activeCraftingMap;
    [SerializeField] InventoryCraftingPanel inventorycraftingPanel;
    [SerializeField] SelectedCraftingPanel selectedCraftingPanel;
    [SerializeField] private Button craftButton;
    [SerializeField] AudioSource m_audioSource;

    ItemInventory playerInventory => GameManager.Instance.assets.playerActor.PlayerItemInventory;

    public SelectedCraftingPanel SelectedCraftingPanel { get => selectedCraftingPanel; }
    public InventoryCraftingPanel InventorycraftingPanel { get => inventorycraftingPanel; }
    public CraftingMap RelicCraftingMap { get => relicCraftinMap; }
    public CraftingMap WeaponCraftingMap { get => weaponCraftingMap; }
    public CraftingMap ActiveCraftingMap { get => activeCraftingMap; }
  
    public void Start()
    {
        selectedCraftingPanel.ManagerAudioSource = m_audioSource;
      //  craftButton.onClick.AddListener(PlayCraftSound);//done in inspector didnt work for some reason
        playerInventory.OnCraftItem.AddListener(inventorycraftingPanel.UpdateAmounts);
        SetUpCraftingScreens();
    }
    public void SelectInventoryItem(ItemUiSlot givenItem)
    {
        if (givenItem.Amount > 0)
        {
            selectedCraftingPanel.AddToPanel(givenItem);
        }
      
    }
    public void PlayCraftSound() => SoundManager.Play(SoundManager.Sound.CraftButtonClicked, m_audioSource);
    public void TurnOnCraftingPanel()
    {
        craftingPanel.SetActive(true);
    }

    public void TurnOffCraftingPanel()
    {
        craftingPanel.SetActive(false);
    }

    //from new buttons
    public void SwitchToWeaponScreen()
    {
        activeCraftingMap = weaponCraftingMap;
        relicMap.SetActive(false);
        weaponMap.SetActive(true);
    }
    public void SwitchToRelicScreen()
    {
        activeCraftingMap = relicCraftinMap;
        weaponMap.SetActive(false);
        relicMap.SetActive(true);
    }
    private void SetUpCraftingScreens()
    {
        inventorycraftingPanel.SetUpSlots(playerInventory.GetEachItemByAmount());
        inventorycraftingPanel.SubscribeSlots();
        selectedCraftingPanel.SubscribeSlots();
    }

    public void SetCraftButton(CraftingMap activeMap)
    {
        craftButton.onClick.RemoveAllListeners();
        craftButton.onClick.AddListener(activeMap.CraftItem);
    }
}
