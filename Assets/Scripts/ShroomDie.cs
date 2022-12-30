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
        handler.Anim.SetTrigger("Die");
        handler.RB.velocity = Vector3.zero;
        base.onStateEnter();
        handler.ShroomActor.RecieveStatusEffects(StatusEffectEnum.Invulnerability);
    }
    public override State RunCurrentState()
    {
        return this;
    }
}
