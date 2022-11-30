using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomWalk : State
{
   // [SerializeField] float stopDuration;
    [SerializeField] float stopCooldown;
    [SerializeField] float lastStop;

    StoneShroomStateHandler handler;
    [SerializeField]  float speed;

    public override void onStateEnter()
    {
        if (handler.AttackMode)
        {
            handler.Anim.SetTrigger(handler.Walkhash);
        }
        else
        {
            handler.Anim.SetTrigger(handler.WalkChash);
        }
    }
    public override State RunCurrentState()
    {
       
        if (handler.ShroomLineOfSight.CanSeePlayer())
        {
            return handler.ShroomNotice;
        }
        if (Time.time - lastStop >= stopCooldown)
        {
            //set animation to idle 
            handler.Anim.SetTrigger(handler.Idlehash);
            lastStop = Time.time;
         // handler.Freeze(stopDuration);
            return handler.ShroomIdle;
        }
     
        if (handler.CheckForFlip())
        {
            handler.MovementDir *= -1;
        }
        handler.RB.velocity = new Vector2(speed * handler.MovementDir, 0);
       
        return this;
    }
    private void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();

    }

}
