using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoRoutineStateHandler : MonoBehaviour
{
    [SerializeField] List<CoRoutineState> states;
    [SerializeField] Actor m_actor;
    private IEnumerator runningStateRoutine;
    private Coroutine mainRoutine;
    private bool active;

    public Actor Actor { get => m_actor; }

    private IEnumerator RunStateMachine()
    {
        while (active)
        {
            runningStateRoutine = GetNextState().StateRoutine();
            Debug.Log(GetNextState().GetType());
            yield return runningStateRoutine;
        }
    }
    private IEnumerator RunStateMachine(CoRoutineState givenState)
    {
        if (!ReferenceEquals(runningStateRoutine, null))
        {
            StopCoroutine(runningStateRoutine);
        }
        if (!ReferenceEquals(mainRoutine, null))
        {
            StopCoroutine(mainRoutine);
        }
        runningStateRoutine = givenState.StateRoutine();
        yield return runningStateRoutine;
        mainRoutine = StartCoroutine(RunStateMachine());
    }

    protected virtual void OnEnable()
    {
        SortStates();
        active = true;
        StartCoroutine(WaitForSetup());
    }

    private IEnumerator WaitForSetup()
    {
        yield return new WaitForSeconds(0.1f);
        mainRoutine = StartCoroutine(RunStateMachine());

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

    public void Interrupt(CoRoutineState givenState)
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(RunStateMachine(givenState));
        }
    }
    //this is the lambda voodoo magic above
    // states.Sort(SortByPriority);
    //static int SortByPriority(CoRoutineState p1, CoRoutineState p2)
    //{
    //    return p1.priority.CompareTo(p2.priority);
    //}
    // [SerializeField] 
}
