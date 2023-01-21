using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomWalkD : BaseShroomDState
{
    [SerializeField] private float walkDuration;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float walkCoolDown;
    private float lastWalked;
    private int walkDir = 1;
    public override IEnumerator StateRoutine()
    {
        float counter = 0;
        handler.Anim.SetBool("Walk", true);
        lastWalked = Time.time;
        handler.Flipper.Disabled = false;
        while (counter < walkDuration)
        {
            if (handler.LineOfSight.IsGrounded())
            {
                handler.Anim.SetBool("Walk", false);
                yield break;
            }
            else if (handler.IsWithinRangeToBounder(2f))
            {
                walkDir *= -1;
            }
            handler.Rb.velocity = new Vector2(walkDir * walkSpeed, handler.Rb.velocity.y);
            counter += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        handler.Anim.SetBool("Walk", false);
    }

    private void Start()
    {
        lastWalked = walkCoolDown * -1;
    }

    internal override bool myCondition()
    {
        if (Time.time - lastWalked >= walkCoolDown)
        {
            return true;
        }
        return false;
    }
}
