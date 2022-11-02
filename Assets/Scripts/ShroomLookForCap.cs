using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomLookForCap : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float lookMovementSpeed;
    public override State RunCurrentState()
    {
        //prioritize getting the cap back
        if (handler.AttackMode && handler.ShroomCap.grouncChecks.IsAtLeastOneGrounded()) //and if the cap is on the ground
        {
            Vector2 dir = new Vector2(handler.ShroomCap.transform.position.x - transform.position.x, 0);
            handler.RB.velocity = dir * lookMovementSpeed;
            return handler.ShroomIdle;
        }
        else
        {
            handler.RB.velocity = Vector2.zero;
            return this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

   
}
