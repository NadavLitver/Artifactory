using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadixIdle : CoRoutineState
{
    internal bool enterTrigger;//set this false to return to idle
    [SerializeField] float idleTime;
    public override IEnumerator StateRoutine()
    {
        enterTrigger = true;
        yield return new WaitForSeconds(idleTime);

    }

    internal override bool myCondition()
    {
        return !enterTrigger;
    }
}
