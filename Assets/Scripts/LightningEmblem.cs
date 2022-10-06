public class LightningEmblem : StatusEffect
{
    private bool buffActive;
    public override void ActivateEffect()
    {
        //nothing to activate
    }

    public override void Reset()
    {
        //nothing to reset here
    }

    public override void Unsubscribe()
    {
        host.OnKill.RemoveListener(ActivateBuff);
        host.OnDealDamage.RemoveListener(EmpowerAttack);
    }

    protected override void Subscribe()
    {
        host.OnKill.AddListener(ActivateBuff);
        host.OnDealDamage.AddListener(EmpowerAttack);
    }

    public void EmpowerAttack(DamageHandler givenDmg, Actor actor)
    {
        if (buffActive)
        {
            givenDmg.AddModifier(GameManager.Instance.DamageManager.LightningEmblemDamageMod);
            buffActive = false;
        }
    }

    public void ActivateBuff(Actor actor)
    {
        buffActive = true;
    }

}
