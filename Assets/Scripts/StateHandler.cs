using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    [SerializeField] State currentState;

    //first state ^
    
    void Update()
    {
       RunStateMachine();
    }
    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if(nextState!= null)
        {
            SwitchToState(nextState);
        }
    }
 
    private void SwitchToState(State _state)
    {
        currentState = _state;
    }
    public void Interrupt(State _state)
    {
        SwitchToState(_state);
    }
}
