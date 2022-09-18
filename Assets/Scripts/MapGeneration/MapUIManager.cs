using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUIManager : MonoBehaviour
{
    [SerializeField] RectTransform largeMapTransfrom;
    [SerializeField] MapTile maptilePrefab;
    public RectTransform LargeMapTransfrom { get => largeMapTransfrom; set => largeMapTransfrom = value; }
    public MapTile MaptilePrefab { get => maptilePrefab; set => maptilePrefab = value; }
}
