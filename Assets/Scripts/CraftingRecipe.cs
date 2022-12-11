using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]

public class CraftingRecipe : ScriptableObject
{
    [SerializeField] List<RecipeCoponent> components = new List<RecipeCoponent>();
    [SerializeField] CraftedItem craftedItem;
    public List<RecipeCoponent> Components { get => components; set => components = value; }
    public CraftedItem CraftedItem { get => craftedItem; set => craftedItem = value; }

}
