using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicInventory : MonoBehaviour
{
    [SerializeField] List <Relic> relicList = new List<Relic>();

    public List<Relic> RelicList { get => relicList;}


    public void AddRelic(Relic givenRelic)
    {
        relicList.Add(givenRelic);
        GameManager.Instance.assets.playerActor.RecieveStatusEffects(givenRelic.MyEffectEnum);
    }



}
