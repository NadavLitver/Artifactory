using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoomLookForCap : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float lookForCapSpeed;
    public override State RunCurrentState()
    {
        //if the handler is in defense mode return idle
        //if the handler isnt in defense mode set velocity in the direction of the cap
        if (!handler.AttackMode)
        {
            return handler.ShroomIdle;
        }
        Vector2 dir = (handler.CurrentCap.transform.position - transform.position).normalized;
        handler.RB.velocity = new Vector2(dir.x * lookForCapSpeed, handler.RB.velocity.y);
        return this;
    }

    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

    

}
