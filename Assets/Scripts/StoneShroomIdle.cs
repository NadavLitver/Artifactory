using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShroomIdle : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float idleSpeed;
    int movementDir = 1;
    public override State RunCurrentState()
    {
        //if we can see the player go into notice player 
        //if the object is grounded, walk along the x axis to the right side. 
        //if the object is not grounded change the direction of movement
        if (handler.ShroomLineOfSight.CanSeePlayer())
        {
            return handler.ShroomNotice;
        }
        if (!handler.ShroomGroundCheck.IsEverythingGrounded())
        {
            movementDir *= -1;
        }

        handler.RB.velocity = new Vector3(idleSpeed * movementDir, handler.RB.velocity.y);
        return this;

    }

    private void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }
}
