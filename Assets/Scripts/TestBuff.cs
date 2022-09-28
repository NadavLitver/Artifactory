public class TestBuff : StatusEffect
{
    public override void ActivateEffect()
    {
        // in this case doest need to activate anything
    }

    public override void Reset()
    {
        //no timer/ effect to reset 
    }

    public override void Unsubscribe()
    {
        host.TempActiveWeapon.OnActorHit.RemoveListener(TestEffect);
    }

    protected override void Subscribe()
    {
        host.TempActiveWeapon.OnActorHit.AddListener(TestEffect);
    }

    private void TestEffect(Actor hitTarget, Ability abilityUsed)
    {
        hitTarget.RecieveStatusEffects(new BurnSE());
        abilityUsed.DamageHandler.myDmgType = DamageType.fire;
        abilityUsed.DamageHandler.AddModifier(1.5f);
    }
}
