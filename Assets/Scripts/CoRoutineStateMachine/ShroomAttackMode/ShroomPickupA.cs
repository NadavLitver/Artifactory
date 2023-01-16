using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomPickupA : ShroomBaseStateA
{
 
    public override IEnumerator StateRoutine()
    {
        handler.Rb.velocity = Vector2.zero;
        handler.Anim.SetTrigger("Pickup");
        yield return new WaitUntil(() => handler.PickedUp);
        handler.PickedUp = false;
        yield return new WaitForEndOfFrame();
        handler.SwitchToOtherMode();
    }

    internal override bool myCondition()
    {
        return true; 
    }

   
}
