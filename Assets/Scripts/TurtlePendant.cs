using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtlePendant : StatusEffect
{
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
        host.onTakeDamage.RemoveListener(ApplyBuff);
    }

    protected override void Subscribe()
    {
        host.onTakeDamage.AddListener(ApplyBuff);
    }

    private void ApplyBuff(DamageHandler givenDmg)
    {
        if (host.currentHP >= GameManager.Instance.DamageManager.TurtleBlockTreshold * host.maxHP)
        {
            givenDmg.AddModifier(GameManager.Instance.DamageManager.TurtleBlockPower);
        }
    }
}
