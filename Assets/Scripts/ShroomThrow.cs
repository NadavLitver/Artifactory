using UnityEngine;

public class ShroomThrow : State
{
    StoneShroomStateHandler handler;
    bool entered;
    public override State RunCurrentState()
    {
        if (!entered)
        {
            entered = true;
            handler.Anim.SetTrigger(handler.Throwhash);
        }
        handler.AttackMode = true;
        handler.RB.velocity = Vector2.zero;
        if (!handler.ReadyToThrow)
        {
            return this;
        }
        ShroomCap cap = handler.GetCapToThrow();
        cap.transform.position = transform.position;
        cap.SetUpPositions(handler.Bounder.MaxPos, handler.Bounder.MinPos);
        cap.gameObject.SetActive(true);
        cap.Throw(new Vector2(handler.GetPlayerDirection().x * handler.ThrowForce, 0));
        handler.Freeze(handler.ThrowDelay);
        handler.ReadyToThrow = false;

        entered = false;
        return handler.ShroomNotice;
    }

    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

}
