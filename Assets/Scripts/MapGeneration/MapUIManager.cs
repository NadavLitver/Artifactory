using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUIManager : MonoBehaviour
{
    [SerializeField] RectTransform largeMapTransfrom;
    [SerializeField] RectTransform minimapTransform;
    [SerializeField] MapTile maptilePrefab;
    [SerializeField] MapTile miniMapTilePrefab;
    public RectTransform LargeMapTransfrom { get => largeMapTransfrom; set => largeMapTransfrom = value; }
    public MapTile MaptilePrefab { get => maptilePrefab; set => maptilePrefab = value; }
    public RectTransform MinimapTransform { get => minimapTransform; set => minimapTransform = value; }
    public MapTile MiniMapTilePrefab { get => miniMapTilePrefab; set => miniMapTilePrefab = value; }
}
