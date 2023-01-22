using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<Room> firstIsleRooms = new List<Room>();
    [SerializeField] List<Room> secondIsleRooms = new List<Room>();
    [SerializeField] List<Room> thirdIsleRooms = new List<Room>();
    [SerializeField] List<Room> currentRunRooms = new List<Room>();
    [SerializeField] List<RunSettings> runSettings = new List<RunSettings>();
    [SerializeField] List<RunSettings> currentRunState = new List<RunSettings>();
    [SerializeField] List<ConnectionData> cachedConnectionDatas = new List<ConnectionData>();
    [SerializeField] Room firstRoom;
    [SerializeField] int numberOfRooms;
    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] int maxNuberOfChests;
    int currentNumberOfChests;
    List<Room> createdRooms = new List<Room>();
    RoomConnectivity roomConnectionCheck = new RoomConnectivity();
    Room active;
    public List<Room> CurrentRunRooms { get => currentRunRooms; }
    public Room FirstRoom { get => firstRoom; set => firstRoom = value; }
    public int NumberOfRooms { get => numberOfRooms; }
    public List<RunSettings> RunSettings { get => runSettings; }
    public List<RunSettings> CurrentRunState { get => currentRunState; set => currentRunState = value; }
    public List<ConnectionData> CachedConnectionDatas { get => cachedConnectionDatas; set => cachedConnectionDatas = value; }
    public Room Active { get => active; set => active = value; }

    private void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();
        InstantiateRooms(firstIsleRooms);
        //  LeanTween.delayedCall(0.5f ,AssembleLevel);
    }

    [ContextMenu("Assemble Level")]
    public void AssembleLevel()
    {
        OrderRooms(createdRooms);
    }

    public void ReorderRoomsClear()
    {
        firstRoom = null;
        currentNumberOfChests = 0;
        currentRunRooms.Clear();
        foreach (var item in createdRooms)
        {
            Destroy(item.gameObject);
        }
        createdRooms.Clear();
        mapGenerator.ClearCreatedTiles();
        InstantiateRooms(firstIsleRooms);
    }

    public void OrderRooms(List<Room> givenRoomList)
    {
        ReorderRoomsClear();
        firstRoom = GetFirstRoom(givenRoomList);
        if (!firstRoom)
        {
            return;
        }
        currentRunRooms.Add(firstRoom);
        firstRoom.Occupied = true;
        UpdateRunSettingsState(firstRoom);
        FirstRoom.AssaignPosition(new CustomPos() { X = 0, Y = 0 });

        int count = 0;
        while (currentRunRooms.Count < numberOfRooms)
        {
            count++;
            if (count > 1000)
            {
                Debug.Log("fell");
                break;
            }
            if (currentRunRooms.Count >= numberOfRooms)
            {
                break;
            }
            Room nextOriginRoom = GetNextOriginRoom();
            ConnectionData newConnection = GetNextConnection(nextOriginRoom, givenRoomList);
            if (newConnection == null)
            {
                /*OrderRooms(givenRoomList);
                return;*/
                continue;
            }
            ConnectRooms(newConnection);
        }
        BeginRun();
    }

    public Room GetFirstRoom(List<Room> givenRoomList)
    {
        List<Room> PossibleStartingRooms = new List<Room>();
        foreach (var item in givenRoomList)
        {
            if (item.IsStartingRoom)
            {
                PossibleStartingRooms.Add(item);
            }
        }
        return PossibleStartingRooms[Random.Range(0, PossibleStartingRooms.Count - 1)];
    }

    public Room GetNextOriginRoom()//get the next room to connect another room to
    {
        List<Room> availableRooms = new List<Room>();
        foreach (var item in currentRunRooms)
        {
            if (!item.AllExitsCoveredCheck())
            {
                availableRooms.Add(item);
            }
        }
        if (availableRooms.Count > 0)
        {
            return availableRooms[Random.Range(0, availableRooms.Count - 1)];
        }
        return null;
    }

    public ConnectionData GetNextConnection(Room originRoom, List<Room> totalRoomList)
    {
        List<ConnectionData> possibleConnections = new List<ConnectionData>();
        foreach (var item in totalRoomList)
        {
            if (item.Occupied || !CheckRoomTypeAvailability(item.RoomType) || !roomConnectionCheck.CheckRoomCennctivity(originRoom, item))
            {
                continue;
            }
            ConnectionData newData = AttemptGettingConnectionData(originRoom, item);
            if (newData != null)
            {
                possibleConnections.Add(newData);
            }
        }
        if (possibleConnections.Count > 0)
        {
            return possibleConnections[Random.Range(0, possibleConnections.Count - 1)];
        }
        return null;
    }

    public ConnectionData AttemptGettingConnectionData(Room origin, Room roomToConnect)
    {
        List<ConnectionData> ConnectionsData = new List<ConnectionData>();
        foreach (var aExit in origin.Exits)
        {
            if (aExit.Occupied)
            {
                continue;
            }
            foreach (var bExit in roomToConnect.Exits)
            {
                if (bExit.Occupied)
                {
                    continue;
                }
                if (aExit.CanConnectToExit(bExit))
                {
                    CustomPos roomToConnectPos = GetPositionFromExits(aExit, bExit);
                    if (CheckPositionAvailability(roomToConnectPos) && CheckPositionAvailability(GetOccupiedPosFromPos(roomToConnectPos, roomToConnect.Size)))
                    {
                        ConnectionPoint pointA = new ConnectionPoint() { Room = origin, Exit = aExit };
                        ConnectionPoint pointB = new ConnectionPoint() { Room = roomToConnect, Exit = bExit };
                        ConnectionsData.Add(new ConnectionData() { PointA = pointA, PointB = pointB });
                    }
                }
            }
        }

        if (ConnectionsData.Count > 0)
        {
            return ConnectionsData[Random.Range(0, ConnectionsData.Count - 1)];
        }

        else return null;
    }

    public bool CheckRoomTypeAvailability(RoomType givenRoomType)
    {
        int roomTypeAmount = 0;
        foreach (var setting in currentRunState)
        {
            if (setting.RoomType == givenRoomType)
            {
                roomTypeAmount = setting.Amount;
                //the current amount of rooms with this type that already were added
            }
        }

        foreach (var setting in RunSettings)
        {
            if (setting.RoomType == givenRoomType && setting.Amount > roomTypeAmount)
            {
                //making sure the room type is valid
                return true;
            }
        }

        return false;
    }

    public bool CheckPositionAvailability(CustomPos givenPos)
    {
        foreach (var item in currentRunRooms)
        {
            if (item.MyPos.EquatePos(givenPos))
            {
                return false;
            }
            foreach (var occupiedPosBySize in item.OccupiedPositions)
            {
                if (occupiedPosBySize.EquatePos(givenPos))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool CheckPositionAvailability(List<CustomPos> givenPositions)
    {
        foreach (var item in givenPositions)
        {
            if (!CheckPositionAvailability(item))
            {
                return false;
            }
        }
        return true;
    }
    public void ConnectRooms(ConnectionData givenConnectionData)
    {
        givenConnectionData.PointA.Exit.ConnectToExit(givenConnectionData.PointB.Exit);
        givenConnectionData.PointB.Exit.ConnectToExit(givenConnectionData.PointA.Exit);
        givenConnectionData.PointB.Room.Occupied = true;
        givenConnectionData.PointB.Room.AssaignPosition(GetPositionFromExits(givenConnectionData.PointA.Exit, givenConnectionData.PointB.Exit));
        currentRunRooms.Add(givenConnectionData.PointB.Room);
        UpdateRunSettingsState(givenConnectionData.PointB.Room);
        cachedConnectionDatas.Add(givenConnectionData);
        // Debug.Log("connected rooms " + givenConnectionData.PointA.Room + " " + givenConnectionData.PointB.Room);
    }

    public void UpdateRunSettingsState(Room addedRoom)
    {
        foreach (var item in currentRunState)
        {
            if (item.RoomType == addedRoom.RoomType)
            {
                item.Amount++;
                return;
            }
        }

        currentRunState.Add(new RunSettings() { RoomType = addedRoom.RoomType, Amount = 1 });

    }

    public CustomPos GetPositionFromExits(ExitInteractable exitA, ExitInteractable exitB)
    {
        CustomPos newRoomPos = new CustomPos() { X = exitA.MyRoom.MyPos.X, Y = exitA.MyRoom.MyPos.Y };

        switch (exitB.ExitLocation.HorizontalPos)
        {
            case ExitLocationHorizontal.LEFT:
                newRoomPos.X++;
                break;
            case ExitLocationHorizontal.RIGHT:
                newRoomPos.X--;
                break;
        }

        switch (exitB.ExitLocation.VerticalPos)
        {
            case ExitLocationVertical.TOP:
                if (exitA.ExitLocation.VerticalPos == ExitLocationVertical.BOTTOM)
                {
                    newRoomPos.Y--;
                }
                break;
            case ExitLocationVertical.BOTTOM:
                if (exitA.ExitLocation.VerticalPos == ExitLocationVertical.TOP)
                {
                    newRoomPos.Y++;
                }
                break;
        }

        return newRoomPos;
    }

    public List<CustomPos> GetOccupiedPosFromPos(CustomPos givenPos, RoomSize givenRoomSize)
    {
        List<CustomPos> occupiedPositions = new List<CustomPos>();

        for (int i = 0; i < givenRoomSize.X; i++)
        {
            for (int j = 0; j < givenRoomSize.Y; j++)
            {
                occupiedPositions.Add(new CustomPos() { X = givenPos.X + i, Y = givenPos.Y + j });
            }
        }
        return occupiedPositions;
    }

    public void InstantiateRooms(List<Room> givenRoomList)
    {
        Vector3 v10 = new Vector3(10, 10, 0);
        foreach (var item in givenRoomList)
        {
            GameObject go = Instantiate(item.gameObject);
            Room r = go.GetComponent<Room>();
            createdRooms.Add(r);
            go.SetActive(false);
        }
    }

    public void BeginRun()
    {
        foreach (var item in currentRunRooms)
        {
            item.gameObject.SetActive(false);
            mapGenerator.AddTile(item);

            foreach (var exit in item.Exits)
            {
                if (ReferenceEquals(exit.OtherExit, null))
                {
                    exit.SetOffExit();
                }
            }
        }

        foreach (var item in CachedConnectionDatas)
        {
            mapGenerator.AddConnection(item);
        }
        SetChests();
        CurrentRunRooms[0].gameObject.SetActive(true);
        active = currentRunRooms[0];
        GameManager.Instance.generalFunctions.SpawnObjectAt(GameManager.Instance.assets.Player.gameObject, active.StartPosition.position);
        mapGenerator.StartRun();
        mapGenerator.UpdateMiniMap(active);
        GameManager.Instance.assets.baseFatherObject.SetActive(false);
        active.OnEntered?.Invoke();
    }


    public void SetChests()
    {
        foreach (var room in CurrentRunRooms)
        {
            if (room.HasChest)
            {
                continue;
            }
            else if (currentNumberOfChests >= maxNuberOfChests)
            {
                break;
            }
            if (room.TrySpawnChest())
            {
                currentNumberOfChests++;
            }
        }
        List<Room> chestFreeRooms = new List<Room>();

        foreach (var item in currentRunRooms)
        {
            if (!item.HasChest && item.ChestSpawnPoints.Count > 0)
            {
                chestFreeRooms.Add(item);
            }
        }

        chestFreeRooms[Random.Range(0, chestFreeRooms.Count)].TrySpawnChest(GameManager.Instance.assets.EmergencyExit);
    }




    public void MoveToRoom(ExitInteractable givenExit)
    {
        active.gameObject.SetActive(false);
        givenExit.OtherExit.MyRoom.gameObject.SetActive(true);
        active = givenExit.OtherExit.MyRoom;
        GameManager.Instance.generalFunctions.SpawnObjectAt(GameManager.Instance.assets.Player.gameObject, givenExit.OtherExit.transform.position);
        mapGenerator.UpdateMiniMap(Active);
        givenExit.OtherExit.MyRoom.OnEntered?.Invoke();
    }
}
