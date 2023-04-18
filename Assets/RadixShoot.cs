using System.Collections;
using UnityEngine;

public class RadixShoot : CoRoutineState
{
    [SerializeField] RadixIdle radixIdle;
    [SerializeField] ObjectPool projectilePool;
    [SerializeField] Transform shootPoint;
    [SerializeField] float timeAfterShootToWaitBeforeIdle;

    [Header("StateCooldownProperties")]
    [SerializeField] float stateCD;
    float lastEnteredState;

    public override IEnumerator StateRoutine()
    {
        GameObject projectile = projectilePool.GetPooledObject();
        projectile.transform.position = shootPoint.position;
        yield return new WaitForSeconds(timeAfterShootToWaitBeforeIdle);
        radixIdle.enterTrigger = false;
    }

    internal override bool myCondition()
    {
      return  Time.time - lastEnteredState > stateCD;
    }
}
