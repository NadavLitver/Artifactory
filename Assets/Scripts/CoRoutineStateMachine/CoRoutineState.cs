using System.Collections;
using UnityEngine;

public abstract class CoRoutineState : MonoBehaviour
{
    //private CoRoutineStateHandler m_Handler;
    //private void Start() => m_Handler = GetComponent<CoRoutineStateHandler>();
    internal abstract bool myCondition();
    public int priority;
    public abstract IEnumerator StateRoutine();
}
