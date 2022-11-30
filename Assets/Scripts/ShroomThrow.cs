using UnityEngine;

public class ShroomThrow : State
{
    StoneShroomStateHandler handler;
    public override void onStateEnter()
    {
        handler.Anim.SetTrigger(handler.Throwhash);
    }
    public override State RunCurrentState()
    {
        handler.RB.velocity = Vector2.zero;
        Vector2 playerDir = handler.GetPlayerDirection();
        if (playerDir.x < 0 && handler.Flipper.IsLookingRight || playerDir.x > 0 && !handler.Flipper.IsLookingRight)
        {
            handler.Flipper.Flip();
        }
        if (!handler.ReadyToThrow)
        {
            return this;
        }
        handler.AttackMode = true;
        ShroomCap cap = handler.GetCapToThrow();
        cap.transform.position = transform.position;
        cap.SetUpPositions(handler.Bounder.MaxPos, handler.Bounder.MinPos);
        cap.gameObject.SetActive(true);
        cap.Throw(new Vector2(playerDir.x * handler.ThrowForce, 1));
        handler.ReadyToThrow = false;
        return handler.ShroomThrowWait;
    }

    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

}
