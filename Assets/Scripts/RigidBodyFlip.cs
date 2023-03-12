using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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


    public UnityEvent OnFlip;
    Coroutine activeRoutine;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void OnEnable()
    {
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
        OnFlip?.Invoke();
        IsLookingRight = false;

        activeRoutine = StartCoroutine(FlipWhenPositive());
    }
    IEnumerator FlipWhenPositive()
    {
        yield return new WaitUntil(() => rb.velocity.x > 0);
        yield return new WaitUntil(() => !Disabled);
        transform.localScale  = positiveVector;
        OnFlip?.Invoke();

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
            OnFlip?.Invoke();
            activeRoutine = StartCoroutine(FlipWhenNegative());
        }
        else
        {
            transform.localScale = negativeVector;
            OnFlip?.Invoke();
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
            OnFlip?.Invoke();
            activeRoutine = StartCoroutine(FlipWhenPositive());
        }
        else
        {
            transform.localScale = positiveVector;
            OnFlip?.Invoke();
        }
    }

}
