using System.Collections.Generic;
using UnityEngine;

public enum CraftingMapType
{
    Relic,
    Weapon
}

public class CraftingMap : MonoBehaviour
{
    [SerializeField] List<CraftingRecipe> knownRecipes = new List<CraftingRecipe>();
    [SerializeField] List<NodeLine> createdLines = new List<NodeLine>();
    [SerializeField] float rotationOffestBetweenLines;
    [SerializeField] CraftingBaseNode baseNode;
    [SerializeField] GameObject CraftButton;
    [SerializeField] private CraftingMapType mapType;
    NodeLine selectedLine;
    [SerializeField] private bool oneTimeCrafts;
    float lineLength;
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

    private void OnEnable()
    {
        GameManager.Instance.CraftingManager.SetCraftButton(this);
    }

    private void CreateLineFromRecipe(CraftingRecipe givenRecipe)
    {
        //loop over all components
        //check if the component still exists somewhere in its specific position
        //keep checking until getting a negative answer
        NodeLine line = new NodeLine();
        line.myRecipe = givenRecipe;
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

        TurnOffLines();


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
            givenLine.LineConnectionPoint = GetConnectionPointFromNode(baseNode);
            givenLine.BaseRotation = givenLine.LineConnectionPoint.GetRotationFromPoint();
        }
        else
        {
            //this is a continuing line
            givenLine.LineConnectionPoint = GetConnectionPointFromNode(givenLine.Nodes[givenLine.Nodes.Count - 1]);
            givenLine.BaseRotation = givenLine.LineConnectionPoint.GetRotationFromPoint();
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
                lastPos = baseNode.transform.localPosition + baseNode.GetConnectionPoint(givenLine.LineConnectionPoint.ConnectionPoint).localPosition;
            }
            else
            {
                lastPos = givenLine.Nodes[i - 1].transform.localPosition + givenLine.Nodes[i - 1].GetConnectionPoint(givenLine.LineConnectionPoint.ConnectionPoint).localPosition;
                //lastPos = givenLine.Nodes[i - 1].GetConnectionPoint(givenLine.LineConnectionPoint.ConnectionPoint).localPosition;
            }

            //get the point to rotate to - 

            node.RotateLine(givenLine.BaseRotation);
            node.transform.localPosition = lastPos;
            node.transform.localPosition += node.CalacDistanceFromSpriteToConnectionPoint();
            node.transform.localScale = Vector3.one;
            //get the distance from the node to its connection point
            //place the node on the previous node
            //move the node by the distance
        }

        //creating the final node
        CraftingMapNode finalNode = CreateNode();
        finalNode.transform.SetParent(transform);
        finalNode.SetUpNode(givenRecipe.CraftedItem.Sprite);
        finalNode.RotateAndSetFinalLine(givenLine.BaseRotation);
        Vector3 pos = givenLine.Nodes[givenLine.Nodes.Count - 1].transform.localPosition + givenLine.Nodes[givenLine.Nodes.Count - 1].GetConnectionPoint(givenLine.LineConnectionPoint.ConnectionPoint).localPosition;
        finalNode.transform.localPosition = pos;
        finalNode.transform.localPosition += finalNode.CalacDistanceFromSpriteToConnectionPoint();
        finalNode.transform.localScale = Vector3.one;

        givenLine.Nodes.Add(finalNode);

        createdLines.Add(givenLine);
    }


    private float GetLineBaseRotation()
    {
        return Random.Range(0, 360);
    }

    private CraftingMapNode CreateNode()
    {
        return Instantiate(GameManager.Instance.generalFunctions.GetCraftingMapNodeFromEnum(mapType));
    }

    private CraftingNodeConnection GetConnectionPointFromNode(CraftingMapNode node)
    {
        Vector3 distanceFromBase = node.transform.position - baseNode.transform.position;
        List<ConnectionPoints> adjacentConnections = node.GetAdjacentConnections();
        List<CraftingNodeConnection> availableValidPoints = new List<CraftingNodeConnection>();
        foreach (var point in node.NodeConnections)
        {
            if (point.Occupied)
            {
                continue;
            }

            foreach (var validPoint in adjacentConnections)
            {
                if (validPoint == point.ConnectionPoint)
                {
                    availableValidPoints.Add(point);
                }
            }
        }
        CraftingNodeConnection finalPoint =  availableValidPoints[Random.Range(0, availableValidPoints.Count)];
        finalPoint.Occupied = true;
        return finalPoint;
    }
    private CraftingNodeConnection GetConnectionPointFromNode(CraftingBaseNode node)
    {
        List<CraftingNodeConnection> occupiedConnections = new List<CraftingNodeConnection>();
        List<CraftingNodeConnection> nonOccupiedConnections = new List<CraftingNodeConnection>();
        foreach (var item in node.NodeConnections)
        {
            if (item.Occupied)
            {
                occupiedConnections.Add(item);
            }
            else
            {
                nonOccupiedConnections.Add(item);
            }
        }
        if (occupiedConnections.Count < 1)
        {   //non of the connection points are occupied
            CraftingNodeConnection selectedPoint = node.NodeConnections[Random.Range(0, node.NodeConnections.Count)];
            selectedPoint.Occupied = true;
            return selectedPoint;
        }
        else
        {
            foreach (var occupiedConnectionPoint in occupiedConnections)
            {
                CraftingNodeConnection selectedPoint = occupiedConnectionPoint;
                ConnectionPoints invertedPoint = selectedPoint.GetInvertedConnectionPointFromPoint(selectedPoint.ConnectionPoint);
                foreach (var connectionPoint in node.NodeConnections)
                {
                    if (connectionPoint.ConnectionPoint == invertedPoint && !connectionPoint.Occupied)
                    {
                        connectionPoint.Occupied = true;
                        return connectionPoint;
                    }
                }
            }
            CraftingNodeConnection finalPoint = nonOccupiedConnections[Random.Range(0, nonOccupiedConnections.Count)];
            finalPoint.Occupied = true;
            return finalPoint;
        }
    }





    public void UpdateActivatedLines(List<ItemType> givenItems)
    {
        //TurnOffLines();

        for (int i = 0; i < givenItems.Count; i++)
        {
            for (int j = 0; j < createdLines.Count; j++)
            {
                if (createdLines[j].Nodes.Count - 1 < i)
                {
                    continue;
                }
                if (givenItems[i] == createdLines[j].Nodes[i].Mycomponent.itemType && IsLineActiveUpTo(createdLines[j], i, givenItems))
                {
                    createdLines[j].Nodes[i]/*.Line*/.gameObject.SetActive(true);
                    createdLines[j].Nodes[i].Cover.SetActive(false);
                    if (createdLines[j].Nodes.Count - 2 == i) //if this is the item before last
                    {
                        //createdLines[j].Nodes[i + 1]/*.Line*/.gameObject.SetActive(true);
                        createdLines[j].Nodes[i + 1].Cover.SetActive(false);
                        selectedLine = createdLines[j];
                        //CraftButton.SetActive(true);
                        //craft button turn on
                        //set this line to be the selected one, 
                        //on press craft item
                    }
                }
            }
        }
    }

    private bool IsLineActiveUpTo(NodeLine givenLine, int index, List<ItemType> givenItems)
    {
        for (int i = 0; i < index; i++)
        {
            if ((!givenLine.Nodes[i].Line.gameObject.activeInHierarchy) || givenLine.Nodes[i].Mycomponent.itemType != givenItems[i])
            {
                return false;
            }
        }
        return true;
    }


    public void TurnOffLines()
    {
        foreach (var line in createdLines)
        {
            for (int i = 1; i < line.Nodes.Count; i++)
            {
                line.Nodes[i].Cover.SetActive(true);
            }
            /* foreach (var node in line.Nodes)
             {
                 node.Cover.SetActive(true);
             }*/
        }
    }

    public void TurnOffLine(NodeLine line)
    {
        foreach (var item in line.Nodes)
        {
            item.gameObject.SetActive(false);
        }
    }


    public void CraftItem() //called from button
    {
        if (ReferenceEquals(selectedLine, null))
        {
            return;
        }
        GameManager.Instance.assets.playerActor.PlayerItemInventory.CraftItem(selectedLine.myRecipe);
        GameManager.Instance.CraftingManager.SelectedCraftingPanel.ClearPanel();
        if (oneTimeCrafts)
        {
            TurnOffLine(selectedLine);
            createdLines.Remove(selectedLine);
        }
        //TurnOffLines();
        //CraftButton.SetActive(false);
    }

}




[System.Serializable]
public class NodeLine
{
    public List<CraftingMapNode> Nodes = new List<CraftingMapNode>();
    public float BaseRotation;
    public bool BaseLine;
    public CraftingNodeConnection LineConnectionPoint;
    public CraftingRecipe myRecipe;
}