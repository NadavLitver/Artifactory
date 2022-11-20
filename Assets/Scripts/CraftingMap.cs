using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMap : MonoBehaviour
{
    //a list of lines, a lines has slots from first to final, each line has its own length
    //when a line is added loop over all other lines and 

    //node prefab 
    //line prefab


    List<CraftingLine> lines = new List<CraftingLine>();

    public List<CraftingLine> Lines { get => lines; set => lines = value; }

    [SerializeField] CraftingMapNode mapBase;
    public CraftingMapNode MapBase { get => mapBase; set => mapBase = value; }

    public void AddRecipeLine(CraftingLine givenLine)
    {
        int maximumPossibleLineSize = givenLine.MyRecipe.Components.Count;
        for (int i = 0; i < maximumPossibleLineSize; i++)
        {
            foreach (CraftingLine item in lines)
            {
                if (item.Nodes.Count <= i)
                {
                    continue;
                }
                if (item.Nodes[i].Mycomponent == givenLine.MyRecipe.Components[i])
                {
                    ConnectLine(item.Nodes[i], givenLine, i);
                    lines.Add(givenLine);
                    return;
                }
            }
        }
        ConnectLine(givenLine);
    }
    private void ConnectLine(CraftingMapNode givenNode, CraftingLine recipeLine, int slotLocation)
    {
        //connect to existing node
        CraftingMapNode currentNode = givenNode;
        for (int i = slotLocation; i < recipeLine.Nodes.Count; i++)
        {
            CraftingMapNode newNode = CreateNode();
            newNode.transform.position = currentNode.Line.transform.position;
            currentNode = newNode;
        }
    }

    private void ConnectLine(CraftingLine recipeLine)
    {
        CraftingMapNode currentNode = mapBase;
        for (int i = 0; i < recipeLine.Nodes.Count; i++)
        {
            CraftingMapNode newNode = CreateNode();
            //setup node 
            newNode.transform.position = currentNode.Line.transform.position;
            currentNode = newNode;
        }
    }

    private CraftingMapNode CreateNode()
    {
        CraftingMapNode newNode = Instantiate(GameManager.Instance.assets.craftingMapNode);
        return newNode;
    }


}
