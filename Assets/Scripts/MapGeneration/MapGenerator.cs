using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<MapTile> createdMapTiles = new List<MapTile>();
    [SerializeField] MapUIManager mapUimanager;


    public List<MapTile> CreatedMapTiles { get => createdMapTiles; set => createdMapTiles = value; }
    public MapUIManager MapUimanager { get => mapUimanager; set => mapUimanager = value; }

    public void AddTile(Room givenRoom)
    {
        MapTile newTile = Instantiate(MapUimanager.MaptilePrefab, MapUimanager.LargeMapTransfrom);
        newTile.CacheRoom(givenRoom);
        createdMapTiles.Add(newTile);
        PlaceTile(newTile);
        newTile.name = newTile.RefRoom.name + " " + newTile.RefRoom.MyPos.ToString();
    }

    public void PlaceTile(MapTile givenTile)
    {
        givenTile.transform.position = new Vector3(givenTile.RefRoom.MyPos.X, givenTile.RefRoom.MyPos.Y, 0) * 10;
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

}
