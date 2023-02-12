using System.Collections.Generic;
using UnityEngine;

public class ZooManager : MonoBehaviour
{
    [SerializeField] private int zooSize;
    [SerializeField] private List<ZooActiveSlot> zooSlots = new List<ZooActiveSlot>();
    [SerializeField] private List<ZooStationGem> gems = new List<ZooStationGem>();
    [SerializeField] private GameObject zooPanel;
    [SerializeField] ZooAnimal test;

    public GameObject ZooPanel { get => zooPanel; }

    public void AddSlot(ZooActiveSlot slot)
    {
        if (zooSlots.Count >= zooSize)
        {
            return;
        }
        zooSlots.Add(slot);
    }

    public void CatchAnimal(ZooAnimal givenAnimal)
    {
        //checking for free space before catching anyway
        ZooAnimalGrowthData newAnimal = new ZooAnimalGrowthData() { animal = givenAnimal, CurrentGrownTime = 0 };
        foreach (var item in zooSlots)
        {
            if (!item.IsOccupied)
            {
                item.CacheAnimal(newAnimal);
                int index = zooSlots.FindIndex(a => zooSlots.Contains(item));
                gems[index].SetGreenActive();
            }
        }
    }

    public void RemoveAnimal(ZooAnimalGrowthData givenAnimal)
    {
        foreach (var item in zooSlots)
        {
            if (ReferenceEquals(item.CurrentRefAnimal, givenAnimal))
            {
                zooSlots.Remove(item);
                return;
            }
        }
    }
    public bool CheckForFreeSpace()
    {
        foreach (var item in zooSlots)
        {
            if (!item.IsOccupied)
            {
                return true;
            }
        }
        return false;
    }

    [ContextMenu("TryCatchingTest")]
    public void TryCatchTest()
    {
        if (CheckForFreeSpace())
        {
            CatchAnimal(test);
        }
    }
}

public class ZooAnimalGrowthData
{
    public ZooAnimal animal;
    public int CurrentGrownTime;
}
