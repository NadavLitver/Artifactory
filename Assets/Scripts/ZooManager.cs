using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class ZooManager : MonoBehaviour
{
    [SerializeField] private int zooSize;
    [SerializeField] private List<ZooActiveSlot> zooSlots = new List<ZooActiveSlot>();
    [SerializeField] private List<ZooStationGem> gems = new List<ZooStationGem>();
    [SerializeField] private GameObject zooPanel;
    [SerializeField] ZooAnimal test;

    public GameObject ZooPanel { get => zooPanel; }
    private void Start()
    {
        LeanTween.delayedCall(2f, TryCatchTest);
    }
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

    public IEnumerator Countdown(float timeToWait, TextMeshProUGUI timerText)
    {
        //count 60 sec for X times
        for (int i = 0; i < timeToWait; i++)
        {
            for (int y = 0; y < 60; y++)
            {
                timerText.text = $" Next Feeding {timeToWait - i} M {60 - y} S";
                yield return new WaitForSecondsRealtime(1f);
            }
        }
    }
}

public class ZooAnimalGrowthData
{
    public ZooAnimal animal;
    public int CurrentGrownTime;
}
