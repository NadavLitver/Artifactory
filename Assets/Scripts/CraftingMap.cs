using System.Collections.Generic;
using UnityEngine;

public class CraftingMap : MonoBehaviour
{
    [SerializeField] List<CraftingRecipe> knownRecipes = new List<CraftingRecipe>();
    [SerializeField] List<NodeLine> createdLines = new List<NodeLine>();
    [SerializeField] Transform mapStartingNode;
    [SerializeField] float rotationOffestBetweenLines;

    private void Start()
    {
        SetUpMap();
    }
    private void SetUpMap()
    {
        foreach (var recipe in knownRecipes)
        {
            CreateLineFromRecipe(recipe);
        }
    }

    private void CreateLineFromRecipe(CraftingRecipe givenRecipe)
    {
        //loop over all components
        //check if the component still exists somewhere in its specific position
        //keep checking until getting a negative answer
        NodeLine line = new NodeLine();

        //loop over to get as many nodes that already exist in place
        for (int i = 0; i < givenRecipe.Components.Count; i++)
        {
            CraftingMapNode existentNode = TryGettingSpecificNodeInPlace(givenRecipe.Components[i], i);
            if (!ReferenceEquals(existentNode, null))
            {
                line.Nodes.Add(existentNode);
            }
            else
            {
                FillLineFromMissingPoint(line, givenRecipe, i);
                break;
            }
        }


    }

    private CraftingMapNode TryGettingSpecificNodeInPlace(RecipeCoponent givenComp, int index)
    {
        for (int i = 0; i < createdLines.Count; i++)
        {
            if (createdLines[i].Nodes.Count <= index)
            {
                continue;
            }
            else if (createdLines[i].Nodes[index].Mycomponent.itemType == givenComp.itemType && createdLines[i].Nodes[index].Mycomponent.amount == givenComp.amount)
            {
                return createdLines[i].Nodes[index];
            }
        }
        return null;
    }

    private void FillLineFromMissingPoint(NodeLine givenLine, CraftingRecipe givenRecipe, int from)
    {
        if (givenLine.Nodes.Count <= 0)
        {
            //this is a new line
            givenLine.BaseLine = true;
            givenLine.BaseRotation = GetLineBaseRotation();
        }
        else
        {
            //this is a continuing line
            givenLine.BaseRotation = GetLineBaseRotation(givenLine.Nodes[givenLine.Nodes.Count-1].Rotation);
        }


        for (int i = from; i < givenRecipe.Components.Count; i++)
        {
            CraftingMapNode node = CreateNode();
            node.transform.SetParent(transform);
            node.SetUpNode(givenRecipe.Components[i]);
            givenLine.Nodes.Add(node);
            Vector3 lastPos;
            if (i == 0)
            {
                lastPos = mapStartingNode.localPosition;
            }
            else
            {
                lastPos = givenLine.Nodes[i - 1].transform.localPosition;
            }

            //rotate line
            node.RotateLine(givenLine.BaseRotation);
            node.transform.localPosition = lastPos;
            node.transform.localPosition += node.CalacDistanceFromSpriteToConnectionPoint();
            //get the distance from the node to its connection point
            //place the node on the previous node
            //move the node by the distance
        }

        //creating the final node
        CraftingMapNode finalNode = CreateNode();
        finalNode.transform.SetParent(transform);
        finalNode.SetUpNode(givenRecipe.CraftedItem.Sprite);
        finalNode.RotateLine(givenLine.BaseRotation);
        Vector3 pos = givenLine.Nodes[givenLine.Nodes.Count - 1].transform.localPosition;
        finalNode.transform.localPosition = pos;
        finalNode.transform.localPosition += finalNode.CalacDistanceFromSpriteToConnectionPoint();
        givenLine.Nodes.Add(finalNode);


        createdLines.Add(givenLine);
    }


    private float GetLineBaseRotation(float previousBase)
    {
        return Random.Range(previousBase - rotationOffestBetweenLines, previousBase + rotationOffestBetweenLines + 1);
    }

    private float GetLineBaseRotation()
    {
        return Random.Range(0,360);
    }

    private CraftingMapNode CreateNode()
    {
        return Instantiate(GameManager.Instance.assets.craftingMapNode);
    }


}

[System.Serializable]
public class NodeLine
{
    public List<CraftingMapNode> Nodes = new List<CraftingMapNode>();
    public float BaseRotation;
    public bool BaseLine;
}