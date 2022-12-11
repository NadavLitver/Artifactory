using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<MapTile> createdMapTiles = new List<MapTile>();
    [SerializeField] List<MapTile> createdMiniMapTiles = new List<MapTile>();
    [SerializeField] MapUIManager mapUimanager;


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
        givenTile.transform.localPosition = new Vector3(givenTile.RefRoom.MyPos.X, givenTile.RefRoom.MyPos.Y, 0) * 130;
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
            MapTile nextMiniMapTile = createdMiniMapTiles[counter];
            pointB = nextMiniMapTile.GetConnectionPointFromExitLocation(exit.OtherExit.ExitLocation);
            counter++;

            if (pointA != null && pointB != null)
            {
                pointA.ConnectLine(pointB.transform);
            }

            //place a node at the right position
            //get connection point from an occupied exit
            //get the other connection point from one of the other nodes correct exit

        }
    }

    

    private MapTile CreateMiniMapTile()
    {
        return Instantiate(MapUimanager.MiniMapTilePrefab, MapUimanager.MinimapTransform);
    }

}


