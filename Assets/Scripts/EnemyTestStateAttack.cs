using UnityEngine;

public class EnemyTestStateAttack : State
{

    public bool canAttack;
    public Animator m_animator;
    public EnemyTestStateChase chaseState;
    public float timeBetweenAttacks;
    private float counter;
    private void Start()
    {
        canAttack = true;
        counter = 0;
    }
    public override State RunCurrentState()
    {
        m_animator.SetBool("Grounded", true);
        m_animator.SetBool("Running", false);
        if (canAttack)
        {
            m_animator.Play("Attack");
            canAttack = false;
            return this;
        }
        if (counter < timeBetweenAttacks)
        {
            counter += Time.deltaTime;
        }
        else
        {
            canAttack = true;
            counter = 0;
        }
        if (!chaseState.isPlayerInAttackRange)
        {
            canAttack = true;
            return chaseState;
        }
        return this;

    }
}
