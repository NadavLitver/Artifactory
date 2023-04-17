using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableEntity : MonoBehaviour
{
    [SerializeField] string id;

    public string Id { get => id; set => id = value; }
    [ContextMenu("Generate Id")]
    private void GenerateId()
    {
        id = Guid.NewGuid().ToString();
    }
    public object SaveState()
    {
        var state = new Dictionary<string, object>();
        ISaveable[] saveables = GetComponents<ISaveable>();
        foreach (var saveable in saveables)
        {
            state[saveable.GetType().ToString()] = saveable.SaveState();
        }
        return state;
    }
    public void LoadState(object state)
    {
        var stateDictionary = (Dictionary<string, object>)state;

        ISaveable[] saveables = GetComponents<ISaveable>();
        foreach (var saveable in saveables)
        {
            string typeName = saveable.GetType().ToString();
            if(stateDictionary.TryGetValue(typeName, out object savedState))
            {
                saveable.LoadState(savedState);
            }
        }
    }
}
