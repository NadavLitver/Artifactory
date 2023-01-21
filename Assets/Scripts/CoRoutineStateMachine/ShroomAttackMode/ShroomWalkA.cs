using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomWalkA : ShroomBaseStateA
{
    [SerializeField] private float walkDuration;
    [SerializeField] private float walkSpeed;
    private int walkDir = 1;
    public override IEnumerator StateRoutine()
    {
        float counter = 0;
        handler.Anim.SetBool("Walk", true);
        handler.Flipper.Disabled = false;
        while (counter < walkDuration)
        {
            if (handler.IsWithinRangeToBounder(2f))
            {
                walkDir *= -1;
            }
            handler.Rb.velocity = new Vector2(walkDir * walkSpeed, handler.Rb.velocity.y);
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        handler.Anim.SetBool("Walk", false);
    }


    internal override bool myCondition()
    {
        return true;
    }
}
