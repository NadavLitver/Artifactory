using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestStateChase : State    
{
    private Transform playerTransform => GameManager.Instance.assets.playerActor.transform;
    public Transform fatherTransform;
    public float AttackRange;
    public float moveSpeed;
    public Animator m_animator;
    public bool isPlayerInAttackRange => GameManager.Instance.generalFunctions.IsInRange(fatherTransform.position,playerTransform.position,AttackRange);
    public EnemyTestStateAttack attackState;
    private Vector3 startScale;
    private void Start()
    {
        startScale = fatherTransform.localScale;
    }
    public override State RunCurrentState()
    {
        if (isPlayerInAttackRange)
        {
            return attackState;
        }
        m_animator.SetBool("Grounded", true);
        m_animator.SetBool("Running", true);
        float xScale = fatherTransform.position.x >= playerTransform.position.x ? -1 : 1;
        fatherTransform.localScale = new Vector3(xScale, startScale.y,startScale.z);
        Vector2 goal = new Vector2(playerTransform.position.x, fatherTransform.position.y);
        fatherTransform.position = Vector2.MoveTowards(fatherTransform.position, goal, Time.deltaTime * moveSpeed);
        return this;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(fatherTransform.position, AttackRange);
    }


}
