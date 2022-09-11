using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestDeathState : State
{
    [SerializeField] EnemyTestActor m_actor;
    [SerializeField] StateHandler m_stateHandler;
    [SerializeField] Animator m_animator;
    bool died;
    public override State RunCurrentState()
    {
        if (!died)
        {
            m_animator.Play("Die");
            died = true;
        }
        return this;
    }
    public void OnDeathInterupt()
    {
        m_stateHandler.Interrupt(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_actor.OnDeath.AddListener(OnDeathInterupt);
    }

   
}
