using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Global Stats")]
    public float BaseCritDamage;


    [Header("Burn Properties"), Space(10)]
    public float burnDuration;
    public float burnDamage;
    [Tooltip("How often do you want the burn to trigger? ")] public float BurnIntervals;

    [Header("Invulnerability Properties"), Space(10)]
    public float InvlunerabilityDuration;

    [Header("Rubber Duck Properties"), Space(10)]
    [Range(1, 100)] public float RubberDuckDamageBlockChance;

    [Header("Lightning Emblem Properties"), Space(10)]
    [Range(1f, 2f)] public float LightningEmblemDamageMod;

    [Header("Healing Goblet Properties"), Space(10)]
    [Range(0.1f, 1f)] public float HealingGobletLifeStealPercentage;

    [Header("Knife of the hunter Properties"), Space(10)]
    [Range(1f, 2f)] public float KnifeOfTheHunterDamageMod;
        

    [Header("Turtle Pendant Propeties"), Space(10)]
    [Range(0f, 1f)] public float TurtleBlockPower;
    [Range(0f, 1f)] public float TurtleBlockTreshold;


    [Header("Legendary Clone Properties"), Space(10)]
    public float MaxHPIncreaseMod;
    public float HealOnKillMod;

}
