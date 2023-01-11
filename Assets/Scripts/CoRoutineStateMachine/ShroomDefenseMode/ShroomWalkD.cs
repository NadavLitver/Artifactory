using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomWalkD : BaseShroomDState
{
    [SerializeField] private float walkDuration;
    [SerializeField] private float walkSpeed;
    private int walkDir = 1;
    public override IEnumerator StateRoutine()
    {
        float counter = 0;  
        //handler.Anim.Play("Walk");
        while (counter < walkDuration)
        {
            if (handler.LineOfSight.IsGrounded())
            {
                yield break;
            }
            else if (handler.IsWithinRangeToBounder(2f))
            {
                walkDir *= -1;
            }
            handler.Rb.velocity = new Vector2(walkDir * walkSpeed, handler.Rb.velocity.y);
            counter += Time.deltaTime;
        }
    }

    internal override bool myCondition()
    {
        return true;
    }
}
