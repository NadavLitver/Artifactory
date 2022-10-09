using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Actor
{
    private void Start()
    {
        OnDeath.AddListener(HealBackToFull);
    }

    public void HealBackToFull()
    {
        currentHP = maxHP;
    }

}
