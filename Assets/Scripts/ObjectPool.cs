using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    private void Awake()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject GO = Instantiate(objectToPool);
            pooledObjects.Add(GO);
            GO.SetActive(false);
        }
    }
    public GameObject GetPooledObject()
    {
        foreach (GameObject go in pooledObjects)
        {
            if (!go.activeInHierarchy)
            {
                return go;
            }
        }
        GameObject GO = Instantiate(objectToPool);
        GO.SetActive(false);
        pooledObjects.Add(GO);
        return GO;
    }
}
