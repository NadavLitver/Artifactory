using System.Collections.Generic;
using UnityEngine;
public class Ability : MonoBehaviour
{
    [Header("NAME")]
    [SerializeField] string Name;
    [Space(10)]
    [SerializeField] DamageHandler damageHandler = new DamageHandler();

    [SerializeField] List<StatusEffectActivatonData> statusEffectsData = new List<StatusEffectActivatonData>();

    [SerializeField, Range(1, 100)] int statusActivationBaseChance;
    [SerializeField] bool useBaseChanceForStatuses;
    private void Awake()
    {
        foreach (StatusEffectActivatonData item in statusEffectsData)
        {
            switch (item.StatusType)
            {
                case StatusEffectEnum.burn:
                    item.myStatus = new BurnSE();
                    break;
                case StatusEffectEnum.freeze:
                    break;
                default:
                    break;
            }
        }
    }

    public bool RollForStatusActivation(StatusEffectActivatonData givenStatus)
    {
        if (useBaseChanceForStatuses)
        {
            if (Random.Range(1, 101) <= statusActivationBaseChance)
            {
                return true;
            }
        }
        else if (Random.Range(1, 101) <= givenStatus.chance)
        {
            return true;
        }

        return false;
    }
    public List<StatusEffectActivatonData> StatusEffects { get => statusEffectsData; set => statusEffectsData = value; }
    public DamageHandler DamageHandler { get => damageHandler; set => damageHandler = value; }
}


[System.Serializable]
public class StatusEffectActivatonData
{
    [Range(1, 100)] public int chance;
    public StatusEffectEnum StatusType;
    public StatusEffect myStatus;
    public StatusEffectActivatonData(int givenChance, StatusEffectEnum givenStatus)
    {
        chance = givenChance;
        StatusType = givenStatus;
    }
}