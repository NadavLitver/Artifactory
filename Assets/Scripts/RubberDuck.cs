using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberDuck : StatusEffect
{
    public override void ActivateEffect()
    {
        //nothing to activate
    }

    public override void Reset()
    {
      //nothing to reset here
    }

    public override void Unsubscribe()
    {
        host.onTakeDamage.RemoveListener(RollForDamageBlock);
    }

    protected override void Subscribe()
    {
        host.onTakeDamage.AddListener(RollForDamageBlock);
    }

    public void RollForDamageBlock(DamageHandler givenDmg)
    {
        if (Random.Range(1, 100) > GameManager.Instance.DamageManager.RubberDuckDamageBlockChance)
        {
            givenDmg.AddModifier(0);
            //play vfx whatever
        }
    }
}
