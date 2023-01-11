using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropChest : Actor
{
    [SerializeField] DragToPlayer myDrop;

    public void Open()
    {
        Instantiate(myDrop, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
