using System.Collections.Generic;
using UnityEngine;

public class ZooManager : MonoBehaviour
{
    [SerializeField] private int zooSize;
    private List<ZooAnimalGrowthData> caughtEnemies = new List<ZooAnimalGrowthData>();
    private List<ZooActiveSlot> zooSlots = new List<ZooActiveSlot>();


    private void Start()
    {
        foreach (var slot in zooSlots)
        {
            GameManager.Instance.OnRunEnd.AddListener(slot.ResetFoodGivenThisInterval);
        }
    }

    public void CatchAnimal(ZooAnimal givenAnimal)
    {
        if (caughtEnemies.Count >= zooSize)
        {
            return;
        }
        ZooAnimalGrowthData newAnimal = new ZooAnimalGrowthData() { animal = givenAnimal, CurrentGrownTime = 0 };
        caughtEnemies.Add(newAnimal);
    }

    public void RemoveAnimal(ZooAnimalGrowthData givenAnimal)
    {
        foreach (var item in zooSlots)
        {
            if (ReferenceEquals(item.CurrentRefAniaml, givenAnimal))
            {
                zooSlots.Remove(item);
                return;
            }
        }
    }
}

public class ZooAnimalGrowthData
{
    public ZooAnimal animal;
    public int CurrentGrownTime;
}
