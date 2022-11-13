using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{
    [Header("NAME")]
    [SerializeField] string Name;
    [Space(10)]
    [SerializeField] DamageHandler damageHandler = new DamageHandler();
    [SerializeField] List<StatusEffectActivatonData> statusEffectsData = new List<StatusEffectActivatonData>();
    [SerializeField, Range(1, 100)] int statusActivationBaseChance;
    [SerializeField] bool useBaseChanceForStatuses;
    [SerializeField] float knockbackForce;
    [SerializeField] float critHitChace;
    [SerializeField] float critHitDamage = 1;
    Vector2 forceDircetion;
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
    public string AbilityName { get => Name; set => Name = value; }
    public float KnockbackForce { get => knockbackForce; set => knockbackForce = value; }
    public float CritHitChace { get => critHitChace; set => critHitChace = value; }
    public float CritHitDamage { get => critHitDamage; set => critHitDamage = value; }

    public void CacheForceDirection(Vector2 givenDirection)
    {
        forceDircetion = givenDirection;
    }
}


[System.Serializable]
public class StatusEffectActivatonData
{
    [Range(1, 100)] public int chance;
    public StatusEffectEnum StatusType;
    public StatusEffectActivatonData(int givenChance, StatusEffectEnum givenStatus)
    {
        chance = givenChance;
        StatusType = givenStatus;
    }
}