using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Burn Properties"), Space(10)]
    public float burnDuration;
    public float burnDamage;
    [Tooltip("How often do you want the burn to trigger? ")] public float BurnIntervals;

    [Header("Rubber Duck Properties"), Space(10)]
    [Range(1, 100)] public float RubberDuckDamageBlockChance;

    [Header("Lightning Emblem Properties"), Space(10)]
    [Range(1f, 2f)] public float LightningEmblemDamageMod;

    [Header("Healing Goblet Properties"), Space(10)]
    [Range(0.1f, 1f)] public float HealingGobletLifeStealPercentage;

}
