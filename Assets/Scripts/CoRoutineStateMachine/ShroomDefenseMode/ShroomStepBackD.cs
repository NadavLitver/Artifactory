using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomStepBackD : BaseShroomDState
{
    [SerializeField] private float stepBackDuration;
    [SerializeField] private float stopSteppingBackThreshold;
    [SerializeField] private float stepBackSpeed;
    public override IEnumerator StateRoutine()
    {
        float counter = 0;
        handler.isEnraged = true;
        handler.Anim.SetBool("StepBack", true);
        while (counter < stepBackDuration)
        {
            if (GameManager.Instance.generalFunctions.IsInRange(transform.position, GameManager.Instance.assets.playerActor.transform.position, stopSteppingBackThreshold))
            {
                EndReset();
                yield break;
            }
            counter += Time.deltaTime;
            handler.Flipper.Disabled = true;
            Vector2 playerDir = (GameManager.Instance.assets.playerActor.transform.position - transform.position).normalized;
            handler.Rb.velocity = new Vector2(playerDir.x * stepBackSpeed * -1, 0);
            yield return new WaitForEndOfFrame();
        }
        EndReset();
    }

    private void EndReset()
    {
        handler.Rb.velocity = Vector3.zero;
        handler.Flipper.Disabled = false;
        handler.Anim.SetBool("StepBack", false);
    }

    internal override bool myCondition()
    {
        if (!handler.isEnraged && handler.LineOfSight.IsGrounded())
        {
            return true;
        }
        return false;
    }
}
