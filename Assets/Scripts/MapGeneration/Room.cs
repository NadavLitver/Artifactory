using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum RoomType
{
    EASY,
    MEDIUM,
    HARD,
    BOSS,
    SPECIAL
    //can add more types in the future - merchant, rest etc...
}

public class Room : MonoBehaviour
{
    [SerializeField] List<ExitInteractable> exits = new List<ExitInteractable>();
    [SerializeField] bool isStartingRoom;
    [SerializeField] bool occupied;
    [SerializeField] RoomType roomType;
    RoomSize size;
    [SerializeField] int sizeX;
    [SerializeField] int sizeY;
    [SerializeField] CustomPos myPos;
    [SerializeField] List<CustomPos> occupiedPositions = new List<CustomPos>();
    [SerializeField] Transform startPosition;
    [SerializeField] List<SpawnPoint> chestSpawnPoints = new List<SpawnPoint>();
    public UnityEvent OnEntered;

    private bool hasChest;
    private bool hasPortal;
    private bool visited;

    public List<ExitInteractable> Exits { get => exits; }
    public bool IsStartingRoom { get => isStartingRoom; }
    public bool Occupied { get => occupied; set => occupied = value; }
    public RoomType RoomType { get => roomType; set => roomType = value; }
    public RoomSize Size { get => size; set => size = value; }
    public CustomPos MyPos { get => myPos; set => myPos = value; }
    public List<CustomPos> OccupiedPositions { get => occupiedPositions; set => occupiedPositions = value; }
    public Transform StartPosition { get => startPosition; set => startPosition = value; }
    public bool HasChest { get => hasChest; set => hasChest = value; }
    public List<SpawnPoint> ChestSpawnPoints { get => chestSpawnPoints; set => chestSpawnPoints = value; }
    public bool Visited { get => visited; }
    public bool HasPortal { get => hasPortal; }

    void Awake()
    {
        size = new RoomSize(sizeX, sizeY);
        ExitInteractable[] exitsFound = GetComponentsInChildren<ExitInteractable>();
        foreach (ExitInteractable exitInteractable in exitsFound)
        {
            exits.Add(exitInteractable);
        }
        foreach (var item in exits)
        {
            item.MyRoom = this;
        }

        ShuffleExits();
        OnEntered.AddListener(FirstVisit);
    }
    public void ShuffleExits()
    {
        int n = exits.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            ExitInteractable value = exits[k];
            exits[k] = exits[n];
            exits[n] = value;
        }
    }

    public bool AllExitsCoveredCheck()
    {
        foreach (var item in exits)
        {
            if (!item.Occupied)
            {
                return false;
            }
        }
        return true;
    }

    public void AssaignPosition(CustomPos givenPos)
    {
        myPos = givenPos;

        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                occupiedPositions.Add(new CustomPos() { X = myPos.X + i, Y = myPos.Y + j });
            }
        }
    }

    public bool TrySpawnChest()
    {
        if (chestSpawnPoints.Count <= 0)
        {
            return false;
        }
        chestSpawnPoints[UnityEngine.Random.Range(0, chestSpawnPoints.Count)].SpawnObject();
        Debug.Log("spawned a chest");
        hasChest = true;
        return true;
    }
    public bool TrySpawnChest(GameObject obj)
    {
        if (chestSpawnPoints.Count <= 0)
        {
            return false;
        }
        Debug.Log("spawned a protal");
        chestSpawnPoints[UnityEngine.Random.Range(0, chestSpawnPoints.Count)].SpawnObject(obj);
        hasPortal = true;
        return true;
    }

    private void FirstVisit()
    {
        if (!visited)
        {
            visited = true;
        }
    }
}
[System.Serializable]
public class RoomSize
{
    // all rooms are 1/1 by default 
    // x marks the amount of rows
    // y marks the amount of collumns
    public int X;
    public int Y;

    public RoomSize(int givenX = 1, int givenY = 1)
    {
        X = givenX;
        Y = givenY;
    }
}

