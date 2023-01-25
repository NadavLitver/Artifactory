using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RigidBodyFlip : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    [SerializeField] private Vector3 positiveVector;
    [SerializeField] private Vector3 negativeVector;

    public bool Disabled;
    public bool IsLookingRight;

    [SerializeField] private bool StartLeft;

    Coroutine activeRoutine;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (StartLeft)
        {
            activeRoutine = StartCoroutine(FlipWhenPositive());
            negativeVector = transform.localScale;
            positiveVector = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            IsLookingRight = false;
        }
        else
        {
            activeRoutine = StartCoroutine(FlipWhenNegative());
            positiveVector = transform.localScale;
            negativeVector = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            IsLookingRight = true;
        }
    }

    IEnumerator FlipWhenNegative()
    { 
        yield return new WaitUntil(() => rb.velocity.x < 0 );
        yield return new WaitUntil(() => !Disabled );
        transform.localScale = negativeVector;
        IsLookingRight = false;

        activeRoutine = StartCoroutine(FlipWhenPositive());
    }
    IEnumerator FlipWhenPositive()
    {
        yield return new WaitUntil(() => rb.velocity.x > 0);
        yield return new WaitUntil(() => !Disabled);
        transform.localScale  = positiveVector;
        IsLookingRight = true;
        activeRoutine = StartCoroutine(FlipWhenNegative());
    }

    public void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        IsLookingRight = !IsLookingRight;
    }

    public void FlipRight()
    {
        if (!ReferenceEquals(activeRoutine, null))
        {
            StopCoroutine(activeRoutine);
        }
        if (StartLeft)
        {
            transform.localScale = positiveVector;
            activeRoutine = StartCoroutine(FlipWhenNegative());
        }
        else
        {
            transform.localScale = negativeVector;
            activeRoutine = StartCoroutine(FlipWhenPositive());
        }
    }

    public void FlipLeft()
    {
        if (!ReferenceEquals(activeRoutine, null))
        {
            StopCoroutine(activeRoutine);
        }
        if (StartLeft)
        {
            transform.localScale = negativeVector;
            activeRoutine = StartCoroutine(FlipWhenPositive());
        }
        else
        {
            transform.localScale = positiveVector;
        }
    }

}
