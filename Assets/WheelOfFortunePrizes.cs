using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WheelOfFortunePrizes : MonoBehaviour
{
    [SerializeField] WheelOfFortuneManager fortuneManager;
    [SerializeField, Range(1, 100)] int HealAmount;
    [SerializeField] GameObject fatherGO;
    
    void Start()
    {
        fortuneManager.OnSpinOverWithWinnerIndex.AddListener(GivePrize);
    }

    private void GivePrize(int index)
    {
        switch (index)
        {
            case 0:
                if (GameManager.Instance.RelicManager.isRelicTaken(StatusEffectEnum.LightningEmblem))
                    return;
                RelicDrop lightEmblem = Instantiate(GameManager.Instance.assets.relicDropPrefab, GameManager.Instance.assets.tuffRef.transform.position, Quaternion.identity);
                lightEmblem.CacheRelic(GameManager.Instance.RelicManager.GetRelic(StatusEffectEnum.LightningEmblem));
                break;
            case 1:
                if (GameManager.Instance.RelicManager.isRelicTaken(StatusEffectEnum.HealingGoblet))
                    return;
                RelicDrop goblet = Instantiate(GameManager.Instance.assets.relicDropPrefab, GameManager.Instance.assets.tuffRef.transform.position, Quaternion.identity);
                goblet.CacheRelic(GameManager.Instance.RelicManager.GetRelic(StatusEffectEnum.HealingGoblet));
                break;
            case 2:
                DropResource(ItemType.Rune);
               
                break;
            case 3:
                DropResource(ItemType.Branch);
                DropResource(ItemType.Glimmering);
                break;
            case 4:
                GameManager.Instance.assets.playerActor.Heal(new DamageHandler() { amount = GameManager.Instance.assets.playerActor.maxHP }); ;
                break;
            case 5:
                //Ragain Golem legs
                break;
            default:
                break;
        }
        fatherGO.SetActive(false);
    }
    private void DropResource()
    {
      
        ItemPickup pickup = Instantiate(GameManager.Instance.assets.ItemPickUpPrefab, GameManager.Instance.assets.tuffRef.transform.position, Quaternion.identity);
        pickup.CacheItemType(GetItem());

    }
    private void DropResource(ItemType item)
    {

        ItemPickup pickup = Instantiate(GameManager.Instance.assets.ItemPickUpPrefab, GameManager.Instance.assets.tuffRef.transform.position, Quaternion.identity);
        pickup.CacheItemType(item);

    }

    private ItemType GetItem()
    {
        ItemType item;
        int temp = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ItemType)).Length - 1);
        item = (ItemType)temp;
        return item;
    }

}
