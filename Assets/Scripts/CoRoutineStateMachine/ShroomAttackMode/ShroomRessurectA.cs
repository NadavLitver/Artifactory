using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomRessurectA : ShroomBaseStateA
{
    public override IEnumerator StateRoutine()
    {
        handler.Anim.SetTrigger("Ressurect");
        handler.Rb.velocity = Vector2.zero;
        transform.parent.transform.position = handler.CurrentCap.transform.position;
        yield return new WaitUntil(() => handler.DoneRessurecting);
        handler.DoneRessurecting = false;
        handler.CurrentCap.gameObject.SetActive(false);
        handler.Actor.Heal(new DamageHandler() { amount = handler.Actor.maxHP });
        handler.SwitchToOtherMode();
    }

    internal override bool myCondition()
    {
        return true;
    }
}
