using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomRessurect : State
{
    StoneShroomStateHandler handler;
    void Start()
    {

        handler = GetComponent<StoneShroomStateHandler>();
    }
    public override void onStateEnter()
    {
        handler.Anim.SetTrigger(handler.RessurectHash);
        handler.RB.velocity = Vector3.zero;
        base.onStateEnter();
    }
    public override State RunCurrentState()
    {
        return this;
    }
}
