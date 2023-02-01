using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomDieA : ShroomBaseStateA
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
        SoundManager.Play(SoundManager.Sound.MushroomEnemyDead, handler.m_audioSource);
        yield return new WaitUntil(() => handler.DoneDying);
        transform.parent.gameObject.SetActive(false);
    }

    internal override bool myCondition()
    {
        return true;
    }
}
