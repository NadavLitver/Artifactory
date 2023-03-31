using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicInventory : MonoBehaviour
{
    [SerializeField] List <Relic> relicList = new List<Relic>();
    [SerializeField] private RelicBar relicBar;

    public List<Relic> RelicList { get => relicList;}


    public void AddRelic(Relic givenRelic)
    {
        relicList.Add(givenRelic);
        GameManager.Instance.assets.prizePanel.CallShowPrizeFromRelic(givenRelic);
        GameManager.Instance.assets.playerActor.RecieveStatusEffects(givenRelic.MyEffectEnum);
        relicBar.AddRelic(givenRelic);
    }



}
