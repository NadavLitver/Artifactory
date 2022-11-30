using UnityEngine;

public class ShroomMoveBackwards : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float noticeOffset;
    [SerializeField] float moveBackwardsSpeed;
    [SerializeField] float walkBackDuration;
    float startedWalkingBack;
    bool enteredState;

    public override void onStateEnter()
    {
        startedWalkingBack = Time.time;
        handler.Flipper.Disabled = true;
        handler.Anim.SetTrigger(handler.WalkBackhash);
    }

    //basing back movement distance on duration and not range because we dont want to get the enemy stuck at a certain distance
    
    
    public override State RunCurrentState()
    {

        if (Time.time - startedWalkingBack >= walkBackDuration || handler.CheckForFlip())
        {
            handler.Enrage();
            handler.MovementDir *= -1;
           
            enteredState = false;
            handler.Flipper.Disabled = false;
            return handler.ShroomWalk;
        }

        else if (handler.isPlayerWithinDefenseRange())
        {
            handler.Enrage();
            enteredState = false;
            handler.Flipper.Disabled = false;
            return handler.ShroomDefense;
        }

        handler.Flipper.Disabled = true;
        handler.RB.velocity = new Vector2(moveBackwardsSpeed * handler.MovementDir * -1, 0);
        return this;
    }


    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }


}
