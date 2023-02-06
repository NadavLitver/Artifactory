using UnityEngine;

[CreateAssetMenu(fileName = "New Animal", menuName = "Animal")]
public class ZooAnimal : ScriptableObject
{
    [SerializeField] private Sprite rSprite;
    [SerializeField] private Sprite wSprite;
    [SerializeField] private int growthThreshold;//how many times can the animal be fed between runs
    [SerializeField] private int totalAmountNeeded;//how many times does the animal needs to be fed to be cleansed
    [SerializeField] private int trannsfromAmount;//how many times does the animal needs to be fed to be transformed
    [SerializeField] private CraftingRecipe food;//the items neseccery for each feeding
    [SerializeField] private int amountOfCoinsOnDrop;
    public int GrowthThreshold { get => growthThreshold; }
    public CraftingRecipe Food { get => food; }
    public int AmountNeeded { get => totalAmountNeeded; }
    public Sprite RSprite { get => rSprite; }
    public Sprite WSprite { get => wSprite; }
    public int AmountOfCoinsOnDrop { get => amountOfCoinsOnDrop; }
    public int TrannsfromAmount { get => trannsfromAmount;  }
}
