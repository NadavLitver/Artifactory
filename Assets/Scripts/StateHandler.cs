using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    [SerializeField] protected State currentState;

    //first state ^
    
    void Update()
    {
       RunStateMachine();
    }
    protected void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if(currentState != nextState)
        {
            nextState.onStateEnter();
        }
      
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
        if (currentState != _state)
        {
            _state.onStateEnter();
        }
        SwitchToState(_state);
    }
   
}
