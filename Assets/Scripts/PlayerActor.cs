using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : Actor
{
    public Animator m_animator;
    public override void onActorDeath()
    {
        base.onActorDeath();
        m_animator.Play("Die");


    }
}
