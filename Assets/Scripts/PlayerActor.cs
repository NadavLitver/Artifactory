using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : Actor
{
    public Animator m_animator;

    private void Start()
    {
        RecieveStatusEffects(new TestBuff());
    }
    public override void onActorDeath()
    {
        base.onActorDeath();
        m_animator.Play("Die");


    }
}
