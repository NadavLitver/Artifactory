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
    public GameObject GetFather()
    {
        return fatherGO;
    }
    private void GivePrize(int index)
    {
        StartCoroutine(GivePrizeRoutine(index));
    }
    IEnumerator GivePrizeRoutine(int index)
    {
       // GameManager.Instance.assets.prizePanel.isDisabled = true;
       // yield return OnWinWheelOfFortuneUI.onWinUIGrow(index);

        switch (index)
        {
            case 0:
                if (GameManager.Instance.RelicManager.isRelicTaken(StatusEffectEnum.LightningEmblem))
                    yield break;
                RelicDrop lightEmblem = Instantiate(GameManager.Instance.assets.relicDropPrefab, GameManager.Instance.assets.tuffRef.transform.position, Quaternion.identity);
                lightEmblem.CacheRelic(GameManager.Instance.RelicManager.GetRelic(StatusEffectEnum.LightningEmblem));
                break;
            case 1:
                if (GameManager.Instance.RelicManager.isRelicTaken(StatusEffectEnum.HealingGoblet))
                    yield break;
                RelicDrop goblet = Instantiate(GameManager.Instance.assets.relicDropPrefab, GameManager.Instance.assets.tuffRef.transform.position, Quaternion.identity);
                goblet.CacheRelic(GameManager.Instance.RelicManager.GetRelic(StatusEffectEnum.HealingGoblet));
                break;
            case 2:
                DropResource(ItemType.Rune,true);

                break;
            case 3:
                DropResource(ItemType.Branch,false);
                DropResource(ItemType.Glimmering, false);
                GameManager.Instance.assets.prizePanel.CallShowPrizeFromSpecialPrize(SpecialPrizes.BranchAndGlimmering);

                break;
            case 4:
                GameManager.Instance.assets.playerActor.Heal(new DamageHandler() { amount = GameManager.Instance.assets.playerActor.maxHP , myDmgType = DamageType.heal});
                GameManager.Instance.assets.prizePanel.CallShowPrizeFromSpecialPrize(SpecialPrizes.Healing);

                break;
            case 5:
                GameManager.Instance.assets.tuffRef.didPlayerGetLegs = true;
                GameManager.Instance.assets.prizePanel.CallShowPrizeFromSpecialPrize(SpecialPrizes.TuffLegs);
                //Ragain Golem legs
                break;
            default:
                break;
        }
       // GameManager.Instance.assets.prizePanel.isDisabled = false;

        TurnOfScreen();
        
    }
    public void TurnOfScreen() =>  fatherGO.SetActive(false);
    private void DropResource()
    {
      
        ItemPickup pickup = Instantiate(GameManager.Instance.assets.ItemPickUpPrefab, GameManager.Instance.assets.tuffRef.transform.position, Quaternion.identity);
        pickup.CacheItemType(GetItem());

    }
    private void DropResource(ItemType item,bool callPrizePanel)
    {

        ItemPickup pickup = Instantiate(GameManager.Instance.assets.ItemPickUpPrefab, GameManager.Instance.assets.tuffRef.transform.position, Quaternion.identity);
        pickup.CacheItemType(item);
        if(callPrizePanel)
          GameManager.Instance.assets.prizePanel.CallShowPrizeFromResource(pickup);


    }
    //private void DropResourceAndCallPrize(ItemType item)
    //{

    //    ItemPickup pickup = Instantiate(GameManager.Instance.assets.ItemPickUpPrefab, GameManager.Instance.assets.tuffRef.transform.position, Quaternion.identity);

    //    pickup.CacheItemType(item);

    //}
    private ItemType GetItem()
    {
        ItemType item;
        int temp = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ItemType)).Length - 1);
        item = (ItemType)temp;
        return item;
    }

}
