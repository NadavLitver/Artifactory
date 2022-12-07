using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBaseNode : MonoBehaviour
{
    [SerializeField] List<CraftingNodeConnection> nodeConnections = new List<CraftingNodeConnection>();

    public List<CraftingNodeConnection> NodeConnections { get => nodeConnections; set => nodeConnections = value; }
}
