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
    private float lastFed;
    [SerializeField] private Image animalImage;
    [SerializeField] private GameObject feedButton;
    [SerializeField] private GameObject releaseButton;
    [SerializeField] private Image resourcePrefab;
    [SerializeField] private Transform resources;
    [SerializeField] private Slider foodSlider;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI sliderText;
    [SerializeField] private float timeToWait; // in mins4
    [SerializeField] private GameObject coinButton;
    [SerializeField] private int startGivenFood;
    [SerializeField] private GameObject renamePanel;
    [SerializeField] private Animator anim;


    [SerializeField] private TMP_InputField inputField;
    private bool timerStarted;
    private bool animalDoneHealing;

    public UnityEvent<ZooAnimalGrowthData> OnAnimalFed;
    public UnityEvent<ZooAnimalGrowthData> OnAnimalFreed;

    public Image AnimalImage { get => animalImage; }
    public ZooAnimalGrowthData CurrentRefAnimal { get => currentRefAnimal; }
    public int CurrentFoodgiven { get => currentFoodgiven; }
    public bool IsOccupied { get => isOccupied; }
    public bool AnimalDoneHealing { get => animalDoneHealing; }

    private void Awake()
    {
        //GameManager.Instance.Zoo.AddSlot(this);
        RemoveAnimal();
        GameManager.Instance.OnRunEnd.AddListener(ResetFoodGivenThisInterval);
    }
    public void CacheAnimal(ZooAnimalGrowthData givenAnimal)
    {
        animalDoneHealing = false;
        currentRefAnimal = givenAnimal;
        animalImage.sprite = givenAnimal.animal.RSprite;
        animalImage.color = new Color(animalImage.color.r, animalImage.color.g, animalImage.color.b, 1);
        nameText.text = givenAnimal.animal.name;
        anim.runtimeAnimatorController = givenAnimal.animal.RAnim;
        timerText.text = "0";
        UpdateTimer();
        currentFoodgiven = 0;
        currentFoodgiven = startGivenFood;
        if (CurrentFoodgiven >= CurrentRefAnimal.animal.TrannsfromAmount)
        {
            ChangeAnimalToWForm();
        }
        ResetFoodGivenThisInterval();
        SetRecipeImage();
        feedButton.SetActive(true);
        isOccupied = true;
        foodSlider.maxValue = CurrentRefAnimal.animal.AmountNeeded;
        UpdateSlider();
    }

    private void OnEnable()
    {
        UpdateTimer();
    }
    private void UpdateTimer()
    {
        if (gameObject.activeInHierarchy && foodGivenThisInterval >= CurrentRefAnimal?.animal.GrowthThreshold && !timerStarted)
        {
            timerStarted = true;
            GameManager.Instance.StartCoroutine(GameManager.Instance.Zoo.Countdown(timeToWait, timerText, this));
        }
        else
        {
            timerText.text = "";
        }
    }

    private void ClearFoodImages()
    {
        for (int i = 0; i < resources.childCount; i++)
        {
            Destroy(resources.GetChild(i).gameObject);
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
        sliderText.text = "  " + ((foodSlider.value / foodSlider.maxValue) * 100).ToString("F1") + "%";
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
        if (ReferenceEquals(CurrentRefAnimal, null))
        {
            return;
        }
        if (foodGivenThisInterval >= CurrentRefAnimal.animal.GrowthThreshold)
        {
            GameManager.Instance.Zoo.GetGemFromSlot(this).SetGreenActive();
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
            animalDoneHealing = true;
            FreeAnimal();
            //releaseButton.SetActive(true);
        }
        else if (CurrentFoodgiven >= CurrentRefAnimal.animal.TrannsfromAmount)
        {
            ChangeAnimalToWForm();
        }
        lastFed = Time.time;
        UpdateSlider();
        UpdateTimer();
    }
 

    //called when reached 100% feed automatically
    public void FreeAnimal()
    {
        feedButton.SetActive(false);
        OnAnimalFreed?.Invoke(CurrentRefAnimal);
        AnimalImage.gameObject.SetActive(false);
        timerText.text = "";
        nameText.text = "";
        ClearFoodImages();
        GameManager.Instance.Zoo.GetGemFromSlot(this).SetGrayActive();
        coinButton.SetActive(true);
    }

    public void CoinButton()
    {
        GameManager.Instance.assets.playerActor.PlayerItemInventory.AddItem(ItemType.TuffCoin);
        GameManager.Instance.Zoo.GetGemFromSlot(this).SetGrayActive();
        coinButton.SetActive(false);
        AnimalImage.gameObject.SetActive(true);
        ResetFoodGiven();
        UpdateSlider();
        RemoveAnimal();

    }


    public void ChangeAnimalToWForm()
    {
        animalImage.sprite = CurrentRefAnimal.animal.WSprite;
        anim.runtimeAnimatorController = CurrentRefAnimal.animal.WAnim;
    }
    public void ResetFoodGiven()
    {
        currentFoodgiven = 0;
    }

    public void ResetFoodGivenThisInterval()
    {
        foodGivenThisInterval = 0;
        timerStarted = false;
    }

    public void ChangeName()
    {
        if (ReferenceEquals(currentRefAnimal, null))
        {
            return;
        }
        renamePanel.SetActive(true);
    }

    public void ConfirmNameChange()
    {
        nameText.text = inputField.text;
        inputField.text = "";
    }
}
