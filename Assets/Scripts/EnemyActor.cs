using System;
using UnityEngine;
public class EnemyActor : Actor
{
    Rigidbody2D rb;

    [SerializeField, Range(0, 100)] private float dropChance;
    [SerializeField] private ItemType item;
    [SerializeField] private bool dropOnDeath;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetUpItemDrop();
        if (dropOnDeath)
        {
            OnDeath.AddListener(DropResource);
        }
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
        Debug.Log("Dropping");
        if (UnityEngine.Random.Range(0, 100) > dropChance)
        {
            return;
        }
        ItemPickup pickup = Instantiate(GameManager.Instance.assets.ItemPickUpPrefab, transform.position, Quaternion.identity);
        pickup.CacheItemType(item);

    }

    private void SetUpItemDrop()
    {
        int temp = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ItemType)).Length - 1);
        item = (ItemType)temp;
    }
}
