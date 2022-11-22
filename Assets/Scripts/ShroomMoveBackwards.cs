using UnityEngine;

public class ShroomMoveBackwards : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float noticeOffset;
    [SerializeField] float moveBackwardsSpeed;
    [SerializeField] float walkBackDuration;
    float startedWalkingBack;
    bool enteredState;

    //basing back movement distance on duration and not range because we dont want to get the enemy stuck at a certain distance
    public override State RunCurrentState()
    {
        if (!enteredState)
        {
            //as we only want to walk back once we cache the moment the state is first called
            enteredState = true;
            startedWalkingBack = Time.time;
        }

        if (Time.time - startedWalkingBack >= walkBackDuration || handler.CheckForFlip())
        {// if walked for the entire duration or if reached a ledge.
            handler.Enrage();
            handler.MovementDir *= -1;
            handler.Flipper.Disabled = false;
            handler.Anim.SetTrigger(handler.Idlehash);

            return handler.ShroomIdle;
        }

        else if (handler.isPlayerWithinDefenseRange())
        {
            handler.Enrage();
            handler.Flipper.Disabled = false;
            handler.Anim.SetTrigger(handler.Defendhash);
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
