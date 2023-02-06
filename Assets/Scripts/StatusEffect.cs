using UnityEngine.Events;


[System.Serializable]
public abstract class StatusEffect
{
    protected Actor host;

   
    public void cacheHost(Actor _host)
    {
        host = _host;
        Subscribe();
        ActivateEffect();
    }
    protected abstract void Subscribe();
    public abstract void Unsubscribe();
    public abstract void Reset();

    public abstract void ActivateEffect();
}
public enum StatusEffectEnum
{
    burn,
    freeze,
    LightningEmblem,
    HealingGoblet,
    Invulnerability,
    TurtlePendant,
    KnifeOfTheHunter,
    WindChimes,
    Legendary
}
