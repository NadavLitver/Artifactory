using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ZooActiveSlot : MonoBehaviour
{
    private ZooAnimalGrowthData currentRefAniaml;
    private int currentFoodgiven;
    private int foodGivenThisInterval;
    private bool isOccupied;

    [SerializeField] private Image animalImage;
    [SerializeField] private GameObject feedButton;
    [SerializeField] private GameObject releaseButton;

    public UnityEvent<ZooAnimalGrowthData> OnAnimalFed;
    public UnityEvent<ZooAnimalGrowthData> OnAnimalFreed;

    public Image AnimalImage { get => animalImage; }
    public ZooAnimalGrowthData CurrentRefAniaml { get => currentRefAniaml; }
    public int CurrentFoodgiven { get => currentFoodgiven; set => currentFoodgiven = value; }
    public bool IsOccupied { get => isOccupied;}

    public void CacheAnimal(ZooAnimalGrowthData givenAnimal)
    {
        currentRefAniaml = givenAnimal;
        animalImage.sprite = givenAnimal.animal.RSprite;
        currentFoodgiven = 0;
        ResetFoodGivenThisInterval();
        feedButton.SetActive(true);
        isOccupied = true;
    }

    public void RemoveAnimal()
    {
        currentRefAniaml = null;
        animalImage.sprite = null;
        isOccupied = false;

    }

    //called from button
    public void FeedSlot()
    {
        if (foodGivenThisInterval >= CurrentRefAniaml.animal.GrowthThreshold)
        {
            return;
        }
        foreach (var comp in CurrentRefAniaml.animal.Food.Components)
        {
            if (!GameManager.Instance.assets.playerActor.PlayerItemInventory.IsComponentAvailable(comp))
            {
                return;
            }
        }
        GameManager.Instance.assets.playerActor.PlayerItemInventory.CraftItem(CurrentRefAniaml.animal.Food);
        foodGivenThisInterval++;
        currentFoodgiven++;
        OnAnimalFed?.Invoke(CurrentRefAniaml);
        if (currentFoodgiven >= CurrentRefAniaml.animal.AmountNeeded)
        {
            animalImage.sprite = CurrentRefAniaml.animal.WSprite;
            //unlock free button
            //on click free and give the player tuff coins
            feedButton.SetActive(false);
            releaseButton.SetActive(true);
        }
    }

    //called from button
    public void FreeAnimal()
    {
        feedButton.SetActive(false);
        OnAnimalFreed?.Invoke(CurrentRefAniaml);
        RemoveAnimal();
    }


    public void ResetFoodGivenThisInterval()
    {
        foodGivenThisInterval = 0;
    }
}
