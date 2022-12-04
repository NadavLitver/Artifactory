using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemUiSlot : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI text;

    public UnityEvent<ItemUiSlot> OnSelected;

    private ItemType myItemType;

    public ItemType MyItemType { get => myItemType; }

    public void SetUpSlot(ItemUiSlot givenSlot)
    {
        myItemType = givenSlot.myItemType;
        itemSprite.sprite = GameManager.Instance.generalFunctions.GetSpriteFromItemType(MyItemType);
    }
    public void SetUpSlot(ItemType giveItem)
    {
        myItemType = giveItem;
        itemSprite.sprite = GameManager.Instance.generalFunctions.GetSpriteFromItemType(myItemType);
    }

    public void SetUpAmount(int amount)
    {
        text.text = amount.ToString();
    }

    public void SelectItem()
    {
        OnSelected?.Invoke(this);
    }

    
}
