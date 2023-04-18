using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicInventory : MonoBehaviour,ISaveable
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

    public void RemoveRelic(Relic givenRelic, bool remove = true)
    {
        if (!RelicList.Contains(givenRelic))
        {
            return;
        }
        GameManager.Instance.assets.playerActor.RemoveStatusEffect(GameManager.Instance.generalFunctions.GetStatusFromType(givenRelic.MyEffectEnum));
        if (remove)
        {
            relicList.Remove(givenRelic);
        }
        relicBar.RemoveRelic(givenRelic);
    }

    public void ClearRelics()
    {
        foreach (var item in relicList)
        {
            RemoveRelic(item, false);
        }
        relicList.Clear();
    }
    public void LoadState(object state)
    {
        var saveData = (MySaveData)state;

        if (saveData.relicEnums != null)
        {
            for (int i = 0; i < saveData.relicEnums.Count; i++)
            {
                Relic current = GameManager.Instance.RelicManager.GetAnyRelicByEnum(saveData.relicEnums[i]);
                relicList.Add(current);
                GameManager.Instance.assets.playerActor.RecieveStatusEffects(current.MyEffectEnum);
                relicBar.AddRelic(current);
            }
        }
    }
    public object SaveState()
    {
        return
           new MySaveData()
           {
               relicEnums = GetListOfEnums()
           };
    }
    public List<StatusEffectEnum> GetListOfEnums()
    {
        List<StatusEffectEnum> effectEnums = new List<StatusEffectEnum>();
        for (int i = 0; i < relicList.Count; i++)
        {
            effectEnums.Add(relicList[i].MyEffectEnum);
        }
        return effectEnums;
    }

    [System.Serializable]
    private struct MySaveData
    {
        public List<StatusEffectEnum> relicEnums;
    }
}
