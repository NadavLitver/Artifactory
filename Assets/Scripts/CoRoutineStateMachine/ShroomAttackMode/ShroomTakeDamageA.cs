using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomTakeDamageA : ShroomBaseStateA
{
    [SerializeField] private float stunDuration;
    [SerializeField] private float knockBackForce;
    public override IEnumerator StateRoutine()
    {
        handler.Anim.SetBool("TakeDmg", true);
        handler.Rb.velocity = Vector3.zero;
        handler.Flipper.Disabled = true;
        handler.Rb.AddForce(handler.GetPlayerDirection() * knockBackForce * -1, ForceMode2D.Impulse);
        yield return new WaitForSecondsRealtime(stunDuration);
        handler.Flipper.Disabled = false;
        handler.Anim.SetBool("TakeDmg", false);
    }


    internal override bool myCondition()
    {
        return true;
    }
}
