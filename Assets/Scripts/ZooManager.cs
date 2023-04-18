using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum ZooAnimalsEnum
{
    Frog, Shroom
}
public class ZooManager : MonoBehaviour
{
    [SerializeField] private int zooSize;
    [SerializeField] private List<ZooActiveSlot> zooSlots = new List<ZooActiveSlot>();
    [SerializeField] private List<ZooStationGem> gems = new List<ZooStationGem>();
    [SerializeField] private GameObject zooPanel;
    [SerializeField] ZooAnimal test;
    [SerializeField] private GameObject spawnCoinParticle;

    [SerializeField] private ZooAnimal shroomTest;
    [SerializeField] private ZooAnimal frogTest;

    public GameObject ZooPanel { get => zooPanel; }

    public void AddZooGem(ZooStationGem gem)
    {
        gems.Add(gem);
    }
    private void Start()
    {
        // Invoke("TryCatchTest", 2f);
        if (SaveLoadSystem.noSaveFile)
            TryCatchTest();
        //    LeanTween.delayedCall(1, TryCatchTest);
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
        // Debug.Log(zooSlots.Count);
    }
    public ZooAnimalGrowthData GetZooAnimalSOBasedOnEnum(ZooAnimalsEnum currentEnum)
    {
        return currentEnum switch
        {
            ZooAnimalsEnum.Frog => new ZooAnimalGrowthData() { animal = frogTest, CurrentFoodGiven = 0 },
            ZooAnimalsEnum.Shroom => new ZooAnimalGrowthData() { animal = shroomTest, CurrentFoodGiven = 0 },
            _ => null,
        };
    }
    public void CatchAnimal(ZooAnimal givenAnimal)
    {
        //checking for free space before catching anyway
        ZooAnimalGrowthData newAnimal = new ZooAnimalGrowthData() { animal = givenAnimal, CurrentFoodGiven = 0 };
        foreach (var item in zooSlots)
        {
            if (!item.IsOccupied)
            {
                item.CacheAnimal(newAnimal);
                /*int index = zooSlots.FindIndex(a => zooSlots.Contains(item));
                ZooStationGem gem = gems[index];
                gem.SetRedActive();*/
                return;
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

    [ContextMenu("Shroom Test")]
    public void TestShroom()
    {
        CatchAnimal(shroomTest);
    }
    [ContextMenu("Frog Test")]
    public void TestFrog()
    {
        CatchAnimal(frogTest);
    }
}
[System.Serializable]
public class ZooAnimalGrowthData
{
    public ZooAnimal animal;
    public int CurrentFoodGiven;
}
