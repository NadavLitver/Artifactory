using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Glimmering,
    Branch,
    Rune,
    Leather,
    Null
}
public class Item : MonoBehaviour
{
    [SerializeField] ItemType item;
    public ItemType ItemType { get => item; set => item = value; }
}
