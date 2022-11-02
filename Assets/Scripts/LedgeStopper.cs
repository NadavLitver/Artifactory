using System.Collections;
using UnityEngine;
using System;

public class LedgeStopper : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GroundCheckCollection groundChecks;

    //when reaching a ledge return the ground check thats triggering the ledge and block 
    //the rigidbody from walking that way

    private void Start()
    {
        StartCoroutine(WaitUntilNotEverythingIsGrounded());
    }

    IEnumerator WaitUntilNotEverythingIsGrounded()
    {
        yield return new WaitUntil(() => !groundChecks.CompletleyGrounded);
        GroundCheck check = groundChecks.FalseGroundCheck();
        if (!ReferenceEquals(check, null))
        {
            if (check.Offset.x > 0 && rb.velocity.x > 0 || check.Offset.x < 0 && rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x*-1, rb.velocity.y);
            }
        }
        StartCoroutine(WaitUntilNotEverythingIsGrounded());
    }
}
