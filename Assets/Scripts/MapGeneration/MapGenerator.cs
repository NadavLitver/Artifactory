using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<MapTile> createdMapTiles = new List<MapTile>();
    [SerializeField] List<MapTile> createdMiniMapTiles = new List<MapTile>();
    [SerializeField] MapUIManager mapUimanager;
    [SerializeField] float miniMapNodeOffset;
    [SerializeField] float largeMapNodeOffsetMod;

    public List<MapTile> CreatedMapTiles { get => createdMapTiles; set => createdMapTiles = value; }
    public MapUIManager MapUimanager { get => mapUimanager; set => mapUimanager = value; }


    private void Start()
    {
        SetUpMiniMap();
    }

    private void SetUpMiniMap()
    {
        for (int i = 0; i < 4; i++)
        {
            createdMiniMapTiles.Add(CreateMiniMapTile());
        }
    }


    public void AddTile(Room givenRoom)
    {
        //large map
        MapTile newTile = Instantiate(MapUimanager.MaptilePrefab, MapUimanager.LargeMapTransfrom);
        newTile.CacheRoom(givenRoom);
        createdMapTiles.Add(newTile);
        newTile.transform.SetParent(mapUimanager.LargeMapTransfrom);
        newTile.SetNodeSize(givenRoom.Size.X, givenRoom.Size.Y);
        PlaceTile(newTile);
        newTile.name = newTile.RefRoom.name + " " + newTile.RefRoom.MyPos.ToString();
    }

    public void PlaceTile(MapTile givenTile)
    {
        givenTile.transform.localPosition = new Vector3(givenTile.RefRoom.MyPos.X, givenTile.RefRoom.MyPos.Y, 0) * mapUimanager.LargeMaptileOffset * largeMapNodeOffsetMod;
    }

    public void PlaceMinimapTile(MapTile givenTile, CustomPos cacledPos)
    {
        givenTile.transform.localPosition = new Vector3(cacledPos.X, cacledPos.Y, 0) * miniMapNodeOffset;
    }

    public void AddConnection(ConnectionData givenConnectionData)
    {
        MapConnectionPoint PointA = null;
        MapConnectionPoint PointB = null;

        foreach (var mapTile in createdMapTiles)
        {
            if (mapTile.RefRoom == givenConnectionData.PointA.Room)
            {
                PointA = mapTile.GetConnectionPointFromExitLocation(givenConnectionData.PointA.Exit.ExitLocation);
            }
            else if (mapTile.RefRoom == givenConnectionData.PointB.Room)
            {
                PointB = mapTile.GetConnectionPointFromExitLocation(givenConnectionData.PointB.Exit.ExitLocation);
            }
        }

        if (PointA != null && PointB != null)
        {
            PointA.gameObject.SetActive(true);
            PointB.gameObject.SetActive(true);
            PointA.ConnectLine(PointB.transform);
        }

    }


    public void UpdateMiniMap(Room activeRoom)
    {
        //recieve active room
        //clear minimap
        //put active room at 0.0 in minimap
        foreach (var item in createdMiniMapTiles)
        {
            item.transform.localPosition = Vector3.zero;
            item.gameObject.SetActive(false);
        }
        mapUimanager.MiniMapCenterNode.CacheRoom(activeRoom);
        foreach (var item in MapUimanager.MiniMapCenterNode.TileConnectionPoints)
        {
            item.gameObject.SetActive(false);
        }

        MapConnectionPoint pointA;
        MapConnectionPoint pointB;
        int counter = 0;
        foreach (var exit in activeRoom.Exits)
        {
            if (!exit.Occupied)
            {
                continue;
            }

            pointA = mapUimanager.MiniMapCenterNode.GetConnectionPointFromExitLocation(exit.ExitLocation);
            pointA.gameObject.SetActive(true);
            MapTile nextMiniMapTile = createdMiniMapTiles[counter];
            nextMiniMapTile.CacheRoom(exit.OtherExit.MyRoom);
            pointB = nextMiniMapTile.GetConnectionPointFromExitLocation(exit.OtherExit.ExitLocation);
            counter++;
            CustomPos newTilePos = new CustomPos() { X = nextMiniMapTile.RefRoom.MyPos.X - activeRoom.MyPos.X, Y = nextMiniMapTile.RefRoom.MyPos.Y - activeRoom.MyPos.Y };
            nextMiniMapTile.gameObject.SetActive(true);
            PlaceMinimapTile(nextMiniMapTile, newTilePos);

            if (pointA != null && pointB != null)
            {
                pointA.ConnectLine(pointB.transform);
            }
        }
    }

    

    private MapTile CreateMiniMapTile()
    {
        return Instantiate(MapUimanager.MiniMapTilePrefab, MapUimanager.MinimapTransform);
    }

}


