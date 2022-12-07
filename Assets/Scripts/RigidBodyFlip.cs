using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RigidBodyFlip : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    Vector3 positiveVector;
    Vector3 negativeVector;

    public bool Disabled;

    public bool IsLookingRight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        positiveVector = transform.localScale;
        negativeVector = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        IsLookingRight = true;
        StartCoroutine(FlipWhenNegative());
    }

    IEnumerator FlipWhenNegative()
    { 
        yield return new WaitUntil(() => rb.velocity.x < 0 );
        yield return new WaitUntil(() => !Disabled );
        transform.localScale = negativeVector;
        IsLookingRight = false;

        StartCoroutine(FlipWhenPositive());
    }
    IEnumerator FlipWhenPositive()
    {
        yield return new WaitUntil(() => rb.velocity.x > 0);
        yield return new WaitUntil(() => !Disabled);
        transform.localScale  = positiveVector;
        IsLookingRight = true;
        StartCoroutine(FlipWhenNegative());
    }

    public void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        IsLookingRight = !IsLookingRight;
    }
}
