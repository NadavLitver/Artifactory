using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomThrowWait : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float ThrowRecovery;
    float counter;
    void Start()
    {
        
        handler = GetComponent<StoneShroomStateHandler>();
    }
    public override State RunCurrentState()
    {
      
        if (counter < ThrowRecovery)
        {
            counter += Time.deltaTime;
            handler.RB.velocity = Vector2.zero;
            return this;
        }
        counter = 0;
        return handler.ShroomRam;
    }
}

