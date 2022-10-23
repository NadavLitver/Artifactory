using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Actor
{
    private void Start()
    {
        OnDeath.AddListener(HealBackToFull);
    }

    [ContextMenu("Heal")]
    public void HealBackToFull()
    {
        Heal(new DamageHandler() { amount = maxHP });
    }

}
