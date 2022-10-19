public class HealingGoblet : StatusEffect
{
    int counter = 0;
    public override void ActivateEffect()
    {

    }

    public override void Reset()
    {

    }

    public override void Unsubscribe()
    {
        host.OnDealingDamageCalcOver.RemoveListener(StealLife);
    }

    protected override void Subscribe()
    {
        host.OnDealingDamageCalcOver.AddListener(StealLife);
    }

    private void StealLife(DamageHandler givenDmg)
    {
        //givenDmg = damage dealt by owner
        if (counter == 10)
        {
            DamageHandler healingDmg = new DamageHandler();
            healingDmg.amount = givenDmg.calculateFinalDamage() * GameManager.Instance.DamageManager.HealingGobletLifeStealPercentage;
            healingDmg.myDmgType = DamageType.heal;
            host.Heal(healingDmg);
            counter = 0;
        }
        else
        {
            counter++;
        }
    }
}
