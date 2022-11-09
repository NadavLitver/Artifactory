using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DamageHandler 
{
    public float amount;
    public DamageType myDmgType;
    List<float> modifiers = new List<float>();
    public float Amount { get => amount; set => amount = value; }

    public void AddModifier(float givenMod)
    {
        modifiers.Add(givenMod);

    }
    public float calculateFinalDamageMult()
    {
        float baseAmount = amount;
        foreach (float mod in modifiers)
        {
            baseAmount *= mod;
        }
        return baseAmount;
    }

    public float calculateFinalDamageAdd()
    {
        float baseAmount = amount;
        foreach (float mod in modifiers)
        {
            baseAmount += mod;
        }
        return baseAmount;
    }
    public void ClearMods()
    {
        modifiers.Clear();
    }
}
public enum DamageType
{
    normal,
    fire,
    ice,
    heal
}
