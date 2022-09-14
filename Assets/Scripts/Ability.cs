using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ability : MonoBehaviour
{
    [SerializeField] DamageHandler damageHandler = new DamageHandler();
    List<StatusEffect> statusEffects = new List<StatusEffect>();
    [SerializeField] List<StatusEffectEnum> statusEffectsEnum = new List<StatusEffectEnum>();

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
    public List<StatusEffect> StatusEffects { get => statusEffects; set => statusEffects = value; }
    public DamageHandler DamageHandler { get => damageHandler; set => damageHandler = value; }
}
