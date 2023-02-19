using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZooManager : MonoBehaviour
{
    [SerializeField] private int zooSize;
    [SerializeField] private List<ZooActiveSlot> zooSlots = new List<ZooActiveSlot>();
    private List<ZooStationGem> gems = new List<ZooStationGem>();
    [SerializeField] private GameObject zooPanel;
    [SerializeField] ZooAnimal test;
    [SerializeField] private GameObject spawnCoinParticle;

    public GameObject ZooPanel { get => zooPanel; }

    public void AddZooGem(ZooStationGem gem)
    {
        gems.Add(gem);
    }
    private void Start()
    {
        TryCatchTest();
    }
    public void AddSlot(ZooActiveSlot slot)
    {
        if (zooSlots.Count >= zooSize)
        {
            return;
        }
        zooSlots.Add(slot);
        int index = zooSlots.FindIndex(a => zooSlots.Contains(slot));
        ZooStationGem gem = gems[index];
        gem.CacheRefSlot(slot);
    }
    public void IncreaseFoodAmount(int amount)
    {
        foreach (var item in zooSlots)
        {
            if (item.IsOccupied)
            {
                for (int i = 0; i < amount; i++)
                {
                    item.QuickFeed();
                }
            }
        }
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
                ZooStationGem gem = gems[index];
                gem.SetRedActive();
            }
        }
    }


    public ZooStationGem GetGemFromSlot(ZooActiveSlot givenSlot)
    {
        foreach (var item in gems)
        {
            if (ReferenceEquals(item.RefSlot, givenSlot))
            {
                return item;
            }
        }
        return null;
    }

    public void SpawnEffectOnCreatureLeaving(ZooActiveSlot givenSlot)
    {
        Instantiate(spawnCoinParticle, givenSlot.AnimalImage.transform.position, Quaternion.identity);
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

    public IEnumerator Countdown(float timeToWait, TextMeshProUGUI timerText, ZooActiveSlot slot)
    {
        //count 60 sec for X times
        for (int i = 0; i < timeToWait; i++)
        {
            for (int y = 0; y < 60; y++)
            {
                if (slot.AnimalDoneHealing)
                {
                    yield break;
                }
                timerText.text = $" Next Feeding {(timeToWait - i).ToString("F1")} M {59 - y} S";
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
