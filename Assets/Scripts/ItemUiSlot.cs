using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemUiSlot : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI text;

    private int amount;

    public UnityEvent<ItemUiSlot> OnSelected;

    private ItemType myItemType;

    public ItemType MyItemType { get => myItemType; }
    public int Amount { get => amount;}

    public void SetUpSlot(ItemUiSlot givenSlot)
    {
        myItemType = givenSlot.myItemType;
        itemSprite.sprite = GameManager.Instance.generalFunctions.GetSpriteFromItemType(MyItemType);
    }

    public void SetUpSlot(ItemUiSlot givenSlot, Color givenColor)
    {
        myItemType = givenSlot.myItemType;
        itemSprite.sprite = GameManager.Instance.generalFunctions.GetSpriteFromItemType(MyItemType);
        itemSprite.color = givenColor;
    }

    public void SetUpSlot(ItemType giveItem)
    {
        myItemType = giveItem;
        itemSprite.sprite = GameManager.Instance.generalFunctions.GetSpriteFromItemType(myItemType);
    }
    public void ResetSlot()
    {
        itemSprite.sprite = null;
        myItemType = ItemType.Null;
        
    }
    public void ResetSlot(Sprite givenSprite)
    {
        itemSprite.sprite = givenSprite;
        itemSprite.color = new Color(0, 0, 0, 0);
        myItemType = ItemType.Null;
    }
    public void SetUpAmount(int amount)
    {
        this.amount = amount;
        text.text = amount.ToString();
    }

    public void SelectItem()
    {
        OnSelected?.Invoke(this);
    }


}
