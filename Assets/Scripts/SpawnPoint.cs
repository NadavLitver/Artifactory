using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Ignore this if using in combat room")]
    [SerializeField] GameObject objectToSpawn;



    public virtual void SpawnObject()
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity, transform.parent);
    }

    public void SpawnObject(GameObject obj)
    {
        Instantiate(obj, transform.position, Quaternion.identity, transform.parent);
    }
    public GameObject SpawnAndGetObject(GameObject obj)
    {
        GameObject vfx = GameManager.Instance.assets.SpawnEnemyVFXOP.GetPooledObject();
        GameObject Obj = Instantiate(obj, transform.position, Quaternion.identity, transform.parent);
        vfx.transform.position = Obj.transform.position;
        vfx.SetActive(true);
        return Obj;
    }
}
