using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZooIneractable : Interactable
{
    public override void Interact()
    {
        GameManager.Instance.Zoo.ZooPanel.SetActive(true);
    }
}
