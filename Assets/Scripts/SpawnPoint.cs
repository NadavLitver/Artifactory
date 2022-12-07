using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject objectToSapwn;
    
    public virtual void SpawnObject()
    {
        Instantiate(objectToSapwn, transform.position, Quaternion.identity, transform.parent);
    }

    public void SpawnObject(GameObject obj)
    {
        Instantiate(obj, transform.position, Quaternion.identity, transform.parent);
    }
}
