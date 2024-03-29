using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomPickup : State
{
    [SerializeField] float pickupDuration;
    float counter;
    StoneShroomStateHandler handler;
    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }
    public override void onStateEnter()
    {
        handler.Anim.SetTrigger(handler.Pickuphash);
    }
    public override State RunCurrentState()
    {
     
        if(counter < pickupDuration)
        {
            counter += Time.deltaTime;
            handler.RB.velocity = Vector2.zero;
            return this;
        }
        counter = 0;
        return handler.ShroomNotice;
    }
}
