using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTile : MonoBehaviour
{
    [SerializeField] Image mapTileImage;
    [SerializeField] List<MapConnectionPoint> tileConnectionPoints;
    [SerializeField] Room refRoom;
    public Image MapTileImage { get => mapTileImage; set => mapTileImage = value; }
    public List<MapConnectionPoint> TileConnectionPoints { get => tileConnectionPoints; set => tileConnectionPoints = value; }
    public Room RefRoom { get => refRoom;}


    public void CacheRoom(Room givenRoom)
    {
        refRoom = givenRoom;
    }

    public MapConnectionPoint GetConnectionPointFromExitLocation(ExitLocationInfo givenLocation)
    {
        foreach (var item in TileConnectionPoints)
        {
            if (item.LocationInfo.EquateLoc(givenLocation))
            {
                return item;
            }
        }
        return null;
    }

    public void SetNodeSize(int x, int y)
    {
        ((RectTransform)mapTileImage.transform).sizeDelta = new Vector2(((RectTransform)mapTileImage.transform).sizeDelta.x * x, ((RectTransform)mapTileImage.transform).sizeDelta.y * y);
    }

    public void SetUpFromExistingMapTile(MapTile givenTile)
    {
        CacheRoom(givenTile.RefRoom);
    }
}
