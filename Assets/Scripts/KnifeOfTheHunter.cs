using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeOfTheHunter : StatusEffect
{
    bool buffed;
    public override void ActivateEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    public override void Unsubscribe()
    {
        host.OnHitRangedAttack.AddListener(ApplyBuff);
    }

    protected override void Subscribe()
    {
        host.OnHitRangedAttack.RemoveListener(ApplyBuff);
    }

    private void ApplyBuff(Ability givenAbility)
    {
        if (buffed)
        {
            return;
        }
        buffed = true;
        host.OnHitMeleeAttack.AddListener(MeleeDamageBuff);
        
    }

    private void MeleeDamageBuff(Ability givenAbility)
    {
        givenAbility.DamageHandler.AddModifier(GameManager.Instance.DamageManager.KnifeOfTheHunterDamageMod);
        buffed = false;
        host.OnHitMeleeAttack.RemoveListener(MeleeDamageBuff);
    }

}
