using System.Collections;
using UnityEngine;

public class RadixRoar : CoRoutineState
{
    [SerializeField] RadixIdle radixIdle;
    RadixSensors radixSensors;
    [Header("StateCooldownProperties")]
    [SerializeField] float stateCD;
    float lastEnteredState;
    [Header("SummonProperties")]
    [SerializeField] ObjectPool prefabToSpawnPool;
    [SerializeField] Vector3 offsetForSummon;
    Vector3 leftOffset => new Vector3(-offsetForSummon.x, offsetForSummon.y);
    [SerializeField] float delayAfterSummon;
    public override IEnumerator StateRoutine()
    {
        lastEnteredState = Time.time;
        GameObject enemy1 = prefabToSpawnPool.GetPooledObject();
        enemy1.SetActive(true);
        enemy1.transform.position = transform.position + offsetForSummon;
        
        GameObject enemy2 = prefabToSpawnPool.GetPooledObject();
        enemy2.SetActive(true);
        enemy2.transform.position = transform.position + leftOffset;
        yield return new WaitForSeconds(delayAfterSummon);
        radixIdle.enterTrigger = false;



    }

    internal override bool myCondition()
    {
        return radixSensors.isRadixUnderHalfHP && Time.time - lastEnteredState > stateCD;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + offsetForSummon, 0.4f);

        Gizmos.DrawWireSphere(transform.position + leftOffset, 0.4f);
    }
}
