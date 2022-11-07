using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Actor
{

    [SerializeField] Ability hitself;
    private void Start()
    {
        OnDeath.AddListener(HealBackToFull);
    }

    [ContextMenu("Heal")]
    public void HealBackToFull()
    {
        Heal(new DamageHandler() { amount = maxHP, myDmgType = DamageType.heal });
    }

    public void hitSelf()
    {
        GetHit(hitself);
    }

}
