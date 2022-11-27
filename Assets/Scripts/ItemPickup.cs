using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : DragToPlayer
{
    [SerializeField] Item myItem;
    public Item MyItem { get => myItem;}

    //add sprite and visuals in the future
    public override void OnDragEnd()
    {
        GameManager.Instance.assets.playerActor.PlayerItemInventory.AddItem(MyItem.ItemType);
        gameObject.SetActive(false);
    }
}
