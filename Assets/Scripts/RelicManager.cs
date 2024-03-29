using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    //a list of cached types of relics
    //once the player picks up a relic add it here
    //once a relic drop is created it will recieve a type thats not cached here
    private List<Relic> takenRelics = new List<Relic>();
    [SerializeField] private List<Relic> freeRelics = new List<Relic>();
    public List<Relic> TakenRelics { get => takenRelics; }
    public List<Relic> FreeRelics { get => freeRelics; }

    public void AddTakenRelic(Relic givenRelic)
    {
        foreach (var item in freeRelics)
        {
            if (item.MyEffectEnum == givenRelic.MyEffectEnum)
            {
                freeRelics.Remove(item);
                break;
            }
        }

        takenRelics.Add(givenRelic);
    }

    public Relic GetFreeRelic()
    {
        if (FreeRelics.Count <= 0)
        {
            Debug.LogError("out of relics");
            return null;
        }
        Relic relic = FreeRelics[Random.Range(0, FreeRelics.Count)];
        AddTakenRelic(relic);
        return relic;
    }
    public Relic GetRelic(StatusEffectEnum requestedRelicEnum)
    {
        foreach (var relic in FreeRelics)
        {
            if (relic.MyEffectEnum == requestedRelicEnum)
            {
                return relic;
            }
        }
        return null;
    }
    public bool isRelicTaken(StatusEffectEnum relicEnum)
    {
        foreach (var relic in TakenRelics)
        {
            if (relic.MyEffectEnum == relicEnum)
            {
                return true;
            }
        }
        return false;
    }
    public Sprite GetRelicSpriteFromRelic(Relic givenRelic)
    {
        switch (givenRelic.MyEffectEnum)
        {
            case StatusEffectEnum.LightningEmblem:
                return GameManager.Instance.assets.LightningEmblem;
            case StatusEffectEnum.HealingGoblet:
                return GameManager.Instance.assets.HealingGoblet;
            case StatusEffectEnum.TurtlePendant:
                return GameManager.Instance.assets.TurtlePendant;
            case StatusEffectEnum.KnifeOfTheHunter:
                return GameManager.Instance.assets.KnifeOfTheHunter;
            case StatusEffectEnum.WindChimes:
                return GameManager.Instance.assets.WindChimes;
            case StatusEffectEnum.Legendary:
                return GameManager.Instance.assets.LegendaryCloneRelic;
            default:
                return null;
        }
    }

}
