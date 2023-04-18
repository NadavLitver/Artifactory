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
        if (RelicList.Contains(givenRelic))
        {
            return;
        }
        relicList.Add(givenRelic);
        GameManager.Instance.assets.prizePanel.CallShowPrizeFromRelic(givenRelic);
        GameManager.Instance.assets.playerActor.RecieveStatusEffects(givenRelic.MyEffectEnum);
        relicBar.AddRelic(givenRelic);
    }

    public void RemoveRelic(Relic givenRelic)
    {
        if (!RelicList.Contains(givenRelic))
        {
            return;
        }
        GameManager.Instance.assets.playerActor.RemoveStatusEffect(GameManager.Instance.generalFunctions.GetStatusFromType(givenRelic.MyEffectEnum));
        relicList.Remove(givenRelic);
    }

    public void ClearRelics()
    {
        foreach (var item in relicList)
        {
            RemoveRelic(item);
        }
        relicList.Clear();
    }
}
