using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropChest : Actor
{
    [SerializeField] RelicDrop myDrop;
    [SerializeField] private Animator anim;
    public void Open()
    {
        anim.Play("Open");
    }

    public void DoneOpening()
    {
        myDrop = Instantiate(GameManager.Instance.assets.relicDropPrefab, transform.position, transform.rotation);
        myDrop.CacheRelic(GameManager.Instance.RelicManager.GetFreeRelic());
    }
}
