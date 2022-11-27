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
            handler.Anim.SetTrigger(handler.Ramhash);
            return handler.ShroomRam;
        }
        else if (handler.ShroomLineOfSight.CanSeePlayer())
        {
            handler.Anim.SetTrigger(handler.Noticehash);
            return handler.ShroomNotice;
        }
        if (handler.CheckForFlip())
        {
            handler.MovementDir *= -1;
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
