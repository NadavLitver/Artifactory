using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShroomDState : CoRoutineState
{
    protected ShroomDefenseHandler handler;
   
    private void Awake()
    {
        handler = GetComponent<ShroomDefenseHandler>();
    }
}
