using System.Collections;
using UnityEngine;

public class BurnSE : StatusEffect
{
    float time;
    DamageHandler m_DamageHandler;
    float counter;

    float intervals;
    public BurnSE()
    {
        m_DamageHandler = new DamageHandler();
        m_DamageHandler.Amount = GameManager.Instance.DamageManager.burnDamage;
        m_DamageHandler.myDmgType = DamageType.fire;
        time = GameManager.Instance.DamageManager.burnDuration;
        intervals =  GameManager.Instance.DamageManager.BurnIntervals;
        counter = 0;

    }

    public override void ActivateEffect()
    {
        host.StartDOT(time, intervals, m_DamageHandler, this);
    }

    public override void Reset()
    {

        counter = 0;
    }

    protected override void Subscribe()
    {
        host.onTakeDamage.AddListener(TakeDamageWhileBurning);
    }

    public override void Unsubscribe()
    {
        host.onTakeDamage.RemoveListener(TakeDamageWhileBurning);
        

    }

    void TakeDamageWhileBurning(DamageHandler dmgHandler)//only happens when already on fire
    {
        if (dmgHandler.myDmgType == DamageType.fire)
        {
            dmgHandler.AddModifier(1.5f);
        }
    }
   
}
