using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CoRoutineStateHandler : MonoBehaviour
{
    [SerializeField] List<CoRoutineState> states;
    [SerializeField] Actor m_actor;
    private IEnumerator runningStateRoutine;
    private bool active;

    private IEnumerator RunStateMachine()
    {
        while (active)
        {
            runningStateRoutine =  GetNextState().StateRoutine();
            yield return runningStateRoutine;
        }
    }
    void Start()
    {
        SortStates();
        active = true;
        StartCoroutine(RunStateMachine());
    }
    [ContextMenu("Sort")]
    private void SortStates()
    {
        states.Sort((p1, p2) => p1.priority.CompareTo(p2.priority));

    }
    public CoRoutineState GetNextState()
    {
        foreach (var state in states)
        {
            if (state.myCondition())
            {
                return state;
            }
        }
        Debug.LogError("Sus Code");
        return null;
    }
    //this is the lambda voodoo magic above
    // states.Sort(SortByPriority);
    //static int SortByPriority(CoRoutineState p1, CoRoutineState p2)
    //{
    //    return p1.priority.CompareTo(p2.priority);
    //}
    // [SerializeField] 
}
