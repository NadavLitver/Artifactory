using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : Actor
{
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void RecieveForce()
    {
        base.RecieveForce();
        
        
    }

    public void HealBackToFull()
    {
        Heal(new DamageHandler() { amount = maxHP, myDmgType = DamageType.heal});
    }
}
