using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public static string SavePath => $"{Application.persistentDataPath}/save.txt";
    public static bool noSaveFile => !File.Exists(SavePath);

    
    SaveableEntity[] saveableEntities;

    private void Awake()
    {
        saveableEntities = FindObjectsOfType<SaveableEntity>();
    }
    [ContextMenu("Save")]
    public void Save()
    {
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
    }
    [ContextMenu("Load")]
    public void Load()
    {
        var state = LoadFile();
        LoadState(state);
    }
    void SaveFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }
    Dictionary<string, object> LoadFile()
    {
        if (noSaveFile)
        {
            Debug.Log("No Save File Found");
            return new Dictionary<string, object>();
        }
        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }
    void SaveState(Dictionary<string, object> state)
    {
       // SaveableEntity[] saveableEntities = FindObjectsOfType<SaveableEntity>();
        foreach (var saveableEntity in saveableEntities)
        {
            state[saveableEntity.Id] = saveableEntity.SaveState();
        }
    }
    void LoadState(Dictionary<string, object> state)
    {

       // SaveableEntity[] saveableEntities = FindObjectsOfType<SaveableEntity>();
        foreach (var saveableEntity in saveableEntities)
        {
            if (state.TryGetValue(saveableEntity.Id, out object savedState))
            {
                saveableEntity.LoadState(savedState);
            }
        }
    }
    [ContextMenu("DeleteSaveFile")]
    public void DeleteSaveFile()
    {
        File.Delete(SavePath);
    }
}
