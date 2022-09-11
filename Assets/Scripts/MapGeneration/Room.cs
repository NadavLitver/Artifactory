using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    EASY,
    MEDIUM,
    HARD,
    BOSS

    //can add more types in the future - merchant, rest etc...
}

public class Room : MonoBehaviour
{
    [SerializeField] List<ExitInteractable> exits = new List<ExitInteractable>();
    [SerializeField] bool isStartingRoom;
    [SerializeField] bool occupied;
    [SerializeField] RoomType roomType;
    [SerializeField] RoomSize size;
    [SerializeField] CustomPos myPos;
    public List<ExitInteractable> Exits { get => exits; }
    public bool IsStartingRoom { get => isStartingRoom; }
    public bool Occupied { get => occupied; set => occupied = value; }
    public RoomType RoomType { get => roomType; set => roomType = value; }
    public RoomSize Size { get => size; set => size = value; }
    public CustomPos MyPos { get => myPos; set => myPos = value; }

    void Awake()
    {
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
    }
    public void ShuffleExits()
    {
        int n = exits.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
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
    }
}
[System.Serializable]
public class RoomSize
{
    public int X;
    public int Y;
}

