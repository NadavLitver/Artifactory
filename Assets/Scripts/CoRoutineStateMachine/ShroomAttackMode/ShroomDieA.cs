using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomDieA : ShroomBaseStateA
{
    public override IEnumerator StateRoutine()
    {
        handler.Rb.velocity = Vector2.zero;
        handler.Anim.SetTrigger("Die");
        yield return new WaitUntil(() => handler.DoneDying);
        transform.parent.gameObject.SetActive(false);
    }

    internal override bool myCondition()
    {
        return true;
    }
}
