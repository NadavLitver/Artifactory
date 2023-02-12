using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ZooActiveSlot : MonoBehaviour
{
    private ZooAnimalGrowthData currentRefAnimal;
    private int currentFoodgiven;
    private int foodGivenThisInterval;
    private bool isOccupied;

    [SerializeField] private Image animalImage;
    [SerializeField] private GameObject feedButton;
    [SerializeField] private GameObject releaseButton;
    [SerializeField] private Image resourcePrefab;
    [SerializeField] private Transform resources;
    [SerializeField] private Slider foodSlider;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeToWait; // in mins

    public UnityEvent<ZooAnimalGrowthData> OnAnimalFed;
    public UnityEvent<ZooAnimalGrowthData> OnAnimalFreed;

    public Image AnimalImage { get => animalImage; }
    public ZooAnimalGrowthData CurrentRefAnimal { get => currentRefAnimal; }
    public int CurrentFoodgiven { get => currentFoodgiven; set => currentFoodgiven = value; }
    public bool IsOccupied { get => isOccupied; }

    private void Start()
    {
        GameManager.Instance.Zoo.AddSlot(this);
        RemoveAnimal();
        GameManager.Instance.OnRunEnd.AddListener(ResetFoodGivenThisInterval);
    }

    public void CacheAnimal(ZooAnimalGrowthData givenAnimal)
    {
        currentRefAnimal = givenAnimal;
        animalImage.sprite = givenAnimal.animal.RSprite;
        animalImage.color = new Color(animalImage.color.r, animalImage.color.g, animalImage.color.b, 1);
        nameText.text = givenAnimal.animal.name;
        timerText.text = "0";
        UpdateTimer();
        currentFoodgiven = 0;
        ResetFoodGivenThisInterval();
        SetRecipeImage();
        feedButton.SetActive(true);
        isOccupied = true;
        foodSlider.maxValue = CurrentRefAnimal.animal.AmountNeeded;
        UpdateSlider();
    }

    private void UpdateTimer()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Countdown());
        }
    }

    private IEnumerator Countdown()
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


    private void SetRecipeImage()
    {
        foreach (var item in CurrentRefAnimal.animal.Food.Components)
        {
            Image newImage = Instantiate(resourcePrefab, resources);
            newImage.sprite = GameManager.Instance.generalFunctions.GetSpriteFromItemType(item.itemType);

        }
    }
    private void UpdateSlider()
    {
        foodSlider.value = currentFoodgiven;
    }

    public void RemoveAnimal()
    {
        currentRefAnimal = null;
        animalImage.sprite = null;
        isOccupied = false;
        animalImage.color = new Color(animalImage.color.r, animalImage.color.g, animalImage.color.b, 0);

    }

    //called from button
    public void FeedSlot()
    {
        if (foodGivenThisInterval >= CurrentRefAnimal.animal.GrowthThreshold)
        {
            return;
        }
        foreach (var comp in CurrentRefAnimal.animal.Food.Components)
        {
            if (!GameManager.Instance.assets.playerActor.PlayerItemInventory.IsComponentAvailable(comp))
            {
                return;
            }
        }
        GameManager.Instance.assets.playerActor.PlayerItemInventory.CraftItem(CurrentRefAnimal.animal.Food);
        foodGivenThisInterval++;
        currentFoodgiven++;
        OnAnimalFed?.Invoke(CurrentRefAnimal);
        if (currentFoodgiven >= CurrentRefAnimal.animal.AmountNeeded)
        {
            animalImage.sprite = CurrentRefAnimal.animal.WSprite;
            //unlock free button
            //on click free and give the player tuff coins
            feedButton.SetActive(false);
            releaseButton.SetActive(true);
        }
        UpdateSlider();
    }

    //called when reached 100% feed automatically
    public void FreeAnimal()
    {
        feedButton.SetActive(false);
        OnAnimalFreed?.Invoke(CurrentRefAnimal);
        RemoveAnimal();
    }


    public void ResetFoodGivenThisInterval()
    {
        foodGivenThisInterval = 0;
    }
}
