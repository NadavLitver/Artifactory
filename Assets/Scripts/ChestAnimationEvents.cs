using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimationEvents : MonoBehaviour
{
    [SerializeField] private DropChest refChest;

    public void DoneOpening()
    {
        refChest.DoneOpening();
    }
}
