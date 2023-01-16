using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShroomBaseStateA : CoRoutineState
{
    protected ShroomSateHandlerA handler;
    private void Awake()
    {
        handler = GetComponent<ShroomSateHandlerA>();
    }
}
