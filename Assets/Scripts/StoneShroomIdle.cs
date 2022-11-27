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
        if (handler.AttackMode) //if the cap is on the floor
        {
            handler.CurrentRamTarget = handler.CurrentCap.transform;
            return handler.ShroomRam;
        }
        else if (handler.ShroomLineOfSight.CanSeePlayer())
        {
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

        //set animation to walk (check if has cap or not to set the right one)
        handler.RB.velocity = new Vector2(idleSpeed * handler.MovementDir, 0);
        return this;
    }

    private void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
        //ramAnimationTriggerHash = Animator.StringToHash("Ram");

    }
}
