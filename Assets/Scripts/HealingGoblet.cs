using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingGoblet : StatusEffect
{
    int counter = 0;
    public override void ActivateEffect()
    {

    }

    public override void Reset()
    {

    }

    public override void Unsubscribe()
    {
        host.OnDealDamage.RemoveListener(StealLife);

    }

    protected override void Subscribe()
    {
        host.OnDealDamage.AddListener(StealLife);
    }

    private void StealLife(DamageHandler givenDmg, Actor givenActor)
    {
        if (counter == 10)
        {
            //heal host 
            counter = 0;
        }
        else
            counter++;
    }
}
