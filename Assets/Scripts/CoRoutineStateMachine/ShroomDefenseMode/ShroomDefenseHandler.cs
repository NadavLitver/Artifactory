using UnityEngine;

public class ShroomDefenseHandler : ShroomBaseHandler
{
    public bool throwFlag;
    public bool isEnraged;
    public AudioSource m_audioSource;

    public override void InterruptTakeDamage(DamageHandler givenDamage)
    {
        isEnraged = true;
        if (givenDamage.calculateFinalNumberMult() > 0)
        {
            base.InterruptTakeDamage(givenDamage);
        }
    }
}
