public class ShroomDefenseHandler : ShroomBaseHandler
{
    public bool throwFlag;
    public bool isEnraged;


    public override void InterruptTakeDamage(DamageHandler givenDamage)
    {
        isEnraged = true;
        if (givenDamage.calculateFinalNumberMult() > 0)
        {
            base.InterruptTakeDamage(givenDamage);
        }
    }
}
