using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomDeathD : BaseShroomDState
{
    private int DieHash;
    private void Start()
    {
        DieHash = Animator.StringToHash("Die");
    }

    public override IEnumerator StateRoutine()
    {
        handler.Rb.velocity = Vector2.zero;
        handler.Anim.SetTrigger(DieHash);
        handler.Actor.RecieveStatusEffects(StatusEffectEnum.Invulnerability);
        SoundManager.Play(SoundManager.Sound.MushroomEnemyDead, handler.m_audioSource);
        yield return new WaitUntil(() => handler.DoneDying);
        handler.Actor.RemoveStatusEffect(new Invulnerability());
        transform.parent.gameObject.SetActive(false);
    }


    internal override bool myCondition()
    {
        return true;
    }
}
