using UnityEngine;

public class StoneShroomIdle : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float stopDuration;
    [SerializeField] float stopCooldown;
    [SerializeField] float lastStop;
    [SerializeField] float idleSpeed;
    bool entered;
  

    public override State RunCurrentState()
    {
        if (handler.AttackMode) //if the cap is on the floor
        {
            entered = false;
            handler.CurrentRamTarget = handler.CurrentCap.transform;
            return handler.ShroomRam;
        }
        else if (handler.ShroomLineOfSight.CanSeePlayer())
        {
            entered = false;
            return handler.ShroomNotice;
        }

        if (handler.CheckForFlip())
        {
            handler.MovementDir *= -1;
        }
        if (Time.time - lastStop >= stopCooldown)
        {
            //set animation to idle 
            handler.Stun(stopDuration);
        }

        if (!entered)
        {
            entered = true;
            if (handler.AttackMode)
            {
                handler.Anim.SetTrigger(handler.Walkhash);
            }
            else
            {
                handler.Anim.SetTrigger(handler.WalkChash);
            }
        }
        handler.RB.velocity = new Vector2(idleSpeed * handler.MovementDir, 0);
        return this;
    }

    private void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
        //ramAnimationTriggerHash = Animator.StringToHash("Ram");

    }
}
