using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class CraftingRecipe : ScriptableObject
{
    List<RecipeCoponent> components = new List<RecipeCoponent>();
    public List<RecipeCoponent> Components { get => components; set => components = value; }
}
