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
        if (handler.ShroomGroundCheck.FlipRequired)
        {
            handler.ShroomGroundCheck.FlipRequired = false;
            movementDir *= -1;
        }
        if (handler.ShroomLineOfSight.CanSeePlayer())
        {
            return handler.ShroomNotice;
        }
        if (handler.AttackMode)//if the shroom doest have its cap
        {
            return handler.ShroomLookForCap;
        }
        handler.RB.velocity = new Vector2(idleSpeed * movementDir , handler.RB.velocity.y);
        return this;

    }

    private void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }
}
