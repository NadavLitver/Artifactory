using System.Collections;
using UnityEngine;

public class RadixMeleeAttack : CoRoutineState
{
    [SerializeField] RadixSensors radixSensors;
    [SerializeField] RadixIdle radixIdle;
    bool AttackFlag;
    [Header("AttackProperties")]
    [SerializeField] int attackCount;
    [SerializeField] Vector3 offsetForSpheres;
    Vector3 leftOffset => new Vector3(-offsetForSpheres.x, offsetForSpheres.y);

    [SerializeField] float sphereSize;
    [SerializeField] protected Ability radixMeleeAttackAbility;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] Vector2 knockbackForce;
    Vector2 leftKnockback => new Vector3(-knockbackForce.x, knockbackForce.y);

    [Header("StateCooldownProperties")]
    [SerializeField] float stateCD;
    float lastEnteredState;

    public override IEnumerator StateRoutine()
    {
        lastEnteredState = Time.time;
        //call animation if its multiple attacks

        for (int i = 0; i < attackCount; i++)
        {
            //call animation if its singel attack
            yield return new WaitUntil(() => AttackFlag);// Call On Attack from animation event
            AttackFlag = false;
        }
        radixIdle.enterTrigger = false;

    }

    public void OnAttack()
    {
        AttackFlag = true;
        bool hitPlayerFromRight = Physics.CheckSphere(transform.position + offsetForSpheres, sphereSize, hitLayer);
        if (!hitPlayerFromRight)
        {
            bool hitPlayerFromLeft = Physics.CheckSphere(transform.position + leftOffset, sphereSize, hitLayer);
            if (hitPlayerFromLeft)
            {
                GameManager.Instance.assets.playerActor.GetHit(radixMeleeAttackAbility);
                GameManager.Instance.assets.PlayerController.RecieveForce(leftKnockback);

            }
        }
        else
        {
            //did hit player
            GameManager.Instance.assets.playerActor.GetHit(radixMeleeAttackAbility);
            GameManager.Instance.assets.PlayerController.RecieveForce(knockbackForce);

        }

    }
    internal override bool myCondition()
    {
        return radixSensors.isPlayerInRangeMelee && Time.time - lastEnteredState > stateCD;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offsetForSpheres, sphereSize);

        Gizmos.DrawWireSphere(transform.position + leftOffset, sphereSize);
    }
}
