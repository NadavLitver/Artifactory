using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoomLookForCap : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float lookForCapSpeed;
    [SerializeField] float ramInsteadOfLookingThreshold;
    public override State RunCurrentState()
    {
        //if the handler is in defense mode return idle
        //if the handler isnt in defense mode set velocity in the direction of the cap
        if (!handler.AttackMode)
        {
            return handler.ShroomIdle;
        }
        if (GameManager.Instance.generalFunctions.IsInRange(GameManager.Instance.assets.playerActor.transform.position, transform.position, ramInsteadOfLookingThreshold))
        {
            return handler.ShroomRam;
        }
        Vector2 dir = (handler.CurrentCap.transform.position - transform.position).normalized;
        handler.RB.velocity = new Vector2(dir.x * lookForCapSpeed, handler.RB.velocity.y);
        return this;
    }

    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, ramInsteadOfLookingThreshold);
    }

}
