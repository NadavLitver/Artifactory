using System.Collections.Generic;
using UnityEngine;

public class CraftingLine : MonoBehaviour
{
    //each line represents a recipe, it is comprised of slots
    //each slot has an item type and an amount, the slots location is the location in the array

    [SerializeField] CraftingRecipe myRecipe;
    public CraftingRecipe MyRecipe { get => myRecipe; }

    [SerializeField] List<CraftingMapNode> nodes = new List<CraftingMapNode>();
    public List<CraftingMapNode> Nodes { get => nodes; }


    public void CahceRecipe(CraftingRecipe givenRecipe)
    {
        myRecipe = givenRecipe;
    }

    public void AddNode(CraftingMapNode givenNode)
    {
        nodes.Add(givenNode);
    }
}
