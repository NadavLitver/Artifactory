using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomTakeDamage : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float stunnedDuration;
    private float counter;
    void Start() 
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }
    public override void onStateEnter()
    {
        counter = 0;
        handler.RB.velocity = Vector3.zero;
        base.onStateEnter();
    }
    public override State RunCurrentState()
    {
        counter += Time.deltaTime;
        if(counter > stunnedDuration)
        {
            return handler.ShroomNotice;
        }
        return this;
    }
}
