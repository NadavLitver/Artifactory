using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : DragToPlayer
{
    [SerializeField] ItemType myItem;
    [SerializeField] SpriteRenderer rend;
    public ItemType MyItem { get => myItem;}

    //add sprite and visuals in the future
    public override void OnDragEnd()
    {
        GameManager.Instance.assets.playerActor.PlayerItemInventory.AddItem(MyItem);
        gameObject.SetActive(false);
    }

    public void CacheItemType(ItemType givenItem)
    {
        myItem = givenItem;
        rend.sprite = GameManager.Instance.generalFunctions.GetSpriteFromItemType(givenItem);
    }

}
