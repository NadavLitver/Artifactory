using System;
using UnityEngine;
public class EnemyActor : Actor
{
    Rigidbody2D rb;

    [SerializeField, Range(0, 100)] private float dropChance;
    [SerializeField] private ItemType item;
    [SerializeField] private bool dropOnDeath;
    [SerializeField] private CatchHandler catchHandler;
    [SerializeField] private ZooAnimal animal;
    private bool dropped;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetUpItemDrop();
        if (dropOnDeath)
        {
            OnDeath.AddListener(DropResource);
        }
        OnDeath.AddListener(AttemptCatching);
    }

    public override void RecieveForce()
    {
        base.RecieveForce();
    }

    public void HealBackToFull()
    {
        Heal(new DamageHandler() { amount = maxHP, myDmgType = DamageType.heal });
    }

    public void DropResource()
    {
        if (UnityEngine.Random.Range(0, 100) > dropChance || dropped)
        {
            return;
        }
        Debug.Log("Dropping");
        ItemPickup pickup = Instantiate(GameManager.Instance.assets.ItemPickUpPrefab, transform.position, Quaternion.identity);
        dropped = true;
        pickup.CacheItemType(item);

    }

    private void SetUpItemDrop()
    {
        int temp = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ItemType)).Length - 1);
        item = (ItemType)temp;
    }

    private void AttemptCatching()
    {
        if (catchHandler.TryCatchingMonster() && GameManager.Instance.Zoo.CheckForFreeSpace())
        {
            Debug.Log(name + " caught");
            GameManager.Instance.Zoo.CatchAnimal(animal);
        }
    }
}
