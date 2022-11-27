using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropChest : Interactable
{
    [SerializeField] DragToPlayer myDrop;

    public override void Interact()
    {
        Instantiate(myDrop, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }


}
