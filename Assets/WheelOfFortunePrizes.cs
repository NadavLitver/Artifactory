using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WheelOfFortunePrizes : MonoBehaviour
{
    [SerializeField] WheelOfFortuneManager fortuneManager;
    [SerializeField, Range(1, 100)] int HealAmount;
    [SerializeField] DamageHandler m_dmgHandler;
    void Start()
    {
        fortuneManager.OnSpinOverWithWinnerIndex.AddListener(GivePrize);
    }

    private void GivePrize(int index)
    {
        switch (index)
        {
            case 0:
                //RelicDrop drop = Instantiate(GameManager.Instance.assets.relicDropPrefab, transform.position, Quaternion.identity, transform);
                //drop.CacheRelic(GameManager.Instance.RelicManager.GetFreeRelic());
                break;
            case 1:
              //  DropResource();
                break;
            case 2:
                //DropResource();
                //DropResource();
                break;
            case 3:
                GameManager.Instance.assets.playerActor.Heal(m_dmgHandler);
                break;
            case 4:
                
                break;
            case 5:
                //Ragain Golem legs
                break;
            default:
                break;
        }
    }
    private void DropResource()
    {
      
        ItemPickup pickup = Instantiate(GameManager.Instance.assets.ItemPickUpPrefab, transform.position, Quaternion.identity);
        pickup.CacheItemType(GetItem());

    }

    private ItemType GetItem()
    {
        ItemType item;
        int temp = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ItemType)).Length - 1);
        item = (ItemType)temp;
        return item;
    }

}
