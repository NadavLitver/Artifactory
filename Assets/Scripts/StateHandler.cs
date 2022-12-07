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
            currentState.onStateExit();
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
       // Debug.Log(_state.GetType().Name);
        if (currentState != _state)
        {
            currentState.onStateExit();
            _state.onStateEnter();
        }
        SwitchToState(_state);
    }
   
}
