using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomDefenseD : BaseShroomDState
{
    [SerializeField] private float defenseThreshold;
    [SerializeField] private float defenseCoolDown;
    private float lastPerformedDefense;
    bool buffOver;
    public override IEnumerator StateRoutine()
    {
        handler.Anim.SetBool("Defend", true);
        handler.Rb.velocity = Vector3.zero;
        handler.LookTowardsPlayer();
        lastPerformedDefense = Time.time;
        buffOver = false;
        handler.Actor.OnStatusEffectRemoved.AddListener(FlagBuffOver);
        handler.Actor.RecieveStatusEffects(StatusEffectEnum.Invulnerability);
        yield return new WaitUntil(() => buffOver);
        buffOver = false;
        handler.Actor.OnStatusEffectRemoved.RemoveListener(FlagBuffOver);
        handler.Anim.SetBool("Defend", false);
    }

    private void Start()
    {
        lastPerformedDefense = defenseCoolDown * -1;
    }

    internal override bool myCondition()
    {
        if (Time.time - lastPerformedDefense >= defenseCoolDown && handler.IsPlayerWithinThreshold(defenseThreshold))
        {
            return true;
        }
        return false;
    }

    private void FlagBuffOver(StatusEffect givenEffect)
    {
        if (givenEffect is Invulnerability)
        {
            buffOver = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, defenseThreshold);
    }
}
