using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ability : MonoBehaviour
{
    [Header("NAME")]
    [SerializeField] string Name;
    [Space(10)]
    [SerializeField] DamageHandler damageHandler = new DamageHandler();
    List<StatusEffect> statusEffects = new List<StatusEffect>();
    [SerializeField] List<StatusEffectEnum> statusEffectsEnum = new List<StatusEffectEnum>();
    [SerializeField, Range(1,10)] int statusActiviationChange;

    private void Awake()
    {
        foreach (StatusEffectEnum item in statusEffectsEnum)
        {
            switch (item)
            {
                case StatusEffectEnum.burn:
                    statusEffects.Add(new BurnSE());
                    break;
                case StatusEffectEnum.freeze:
                    break;
                default:
                    break;
            }
        }
    }

    public bool RollForStatusActivation()
    {
        if (Random.Range(1,11) <= statusActiviationChange)
        {
            return true;
        }
        return false;
    }
    public List<StatusEffect> StatusEffects { get => statusEffects; set => statusEffects = value; }
    public DamageHandler DamageHandler { get => damageHandler; set => damageHandler = value; }
}
