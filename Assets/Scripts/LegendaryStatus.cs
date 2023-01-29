using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendaryStatus : StatusEffect
{
    public override void ActivateEffect()
    {
        //nothing to activate here
    }

    public override void Reset()
    {
        //no reset here
    }

    public override void Unsubscribe()
    {
        host.maxHP /= GameManager.Instance.DamageManager.MaxHPIncreaseMod;
        host.OnKill.RemoveListener(HealOnKill);
        host.OnDeath.RemoveListener(StartSpinningWheel);
    }

    protected override void Subscribe()
    {
        host.maxHP *= GameManager.Instance.DamageManager.MaxHPIncreaseMod;
        host.OnKill.AddListener(HealOnKill);
        host.OnDeath.AddListener(StartSpinningWheel);
    }

    private void HealOnKill(Actor target, DamageHandler givenDamage)
    {
        host.Heal(new DamageHandler() { amount = givenDamage.calculateFinalNumberMult() });
    }

    private void StartSpinningWheel()
    {
        //basically pause game spin wheel and see what happens.
    }
}

