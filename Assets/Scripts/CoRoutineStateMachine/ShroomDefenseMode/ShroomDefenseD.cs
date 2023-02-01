using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomDefenseD : BaseShroomDState
{
    [SerializeField] private float defenseThreshold;
    [SerializeField] private float defenseCoolDown;
    private float lastPerformedDefense;
    bool buffOver;
    private int DefendHash;

    public override IEnumerator StateRoutine()
    {
        handler.Anim.SetBool(DefendHash, true);
        handler.Rb.velocity = Vector3.zero;
        handler.LookTowardsPlayer();
        lastPerformedDefense = Time.time;
        buffOver = false;
        handler.Actor.OnStatusEffectRemoved.AddListener(FlagBuffOver);
        handler.Actor.RecieveStatusEffects(StatusEffectEnum.Invulnerability);
        handler.Actor.OnGetHit.AddListener(OnDefendedAttack);
        yield return new WaitUntil(() => buffOver);
        buffOver = false;
        handler.Actor.OnGetHit.RemoveListener(OnDefendedAttack);
        handler.Actor.OnStatusEffectRemoved.RemoveListener(FlagBuffOver);
        handler.Anim.SetBool(DefendHash, false);
    }
    private void OnDefendedAttack(Ability _ability)
    {
        SoundManager.Play(SoundManager.Sound.MushroomEnemyBlocked,handler.m_audioSource);
    }
    private void Start()
    {
        DefendHash = Animator.StringToHash("Defend");
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
