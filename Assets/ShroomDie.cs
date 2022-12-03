using UnityEngine;

public class ShroomDie : State
{
    StoneShroomStateHandler handler;
    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }
    public override void onStateEnter()
    {
        handler.Anim.SetTrigger(handler.Diehash);
        handler.RB.velocity = Vector3.zero;
        base.onStateEnter();
    }
    public override State RunCurrentState()
    {
        return this;
    }
}
