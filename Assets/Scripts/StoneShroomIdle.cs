using UnityEngine;

public class StoneShroomIdle : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float stopDuration;
    [SerializeField] float stopCooldown;
    [SerializeField] float lastStop;

    [SerializeField] float idleSpeed;
    public override State RunCurrentState()
    {
        //if we can see the player go into notice player 
        //if the object is grounded, walk along the x axis to the right side. 
        //if the object is not grounded change the direction of movement
        handler.Flipper.Disabled = false;
        /*if (Time.time - lastStop >= stopCooldown)
        {
            lastStop = Time.time;
            handler.Freeze(stopDuration);
        }*/
        if (handler.ShroomGroundCheck.FlipRequired)
        {
            handler.ShroomGroundCheck.FlipRequired = false;
            handler.MovementDir *= -1;
        }
        if (handler.AttackMode)//if the shroom doest have its cap
        {
            return handler.ShroomLookForCap;
        }
        else if (handler.ShroomLineOfSight.CanSeePlayer())
        {
            return handler.ShroomNotice;
        }
        handler.RB.velocity = new Vector2(idleSpeed * handler.MovementDir, handler.RB.velocity.y);
        return this;

    }

    private void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }
}
