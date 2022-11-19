using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShroomRam : State
{
    [SerializeField] float ramSpeed;
    [SerializeField] float ramTargetOffset;
    [SerializeField] float ramRecoveryTime;
    StoneShroomStateHandler handler;
    Vector3 currentDest;
    bool StartedCharging;
    bool tookDamage;
    public override State RunCurrentState()
    {
        Debug.Log("ramming");
        if (!StartedCharging)
        {
            handler.ShroomActor.TakeDamageGFX.AddListener(TurnOnTookDamage);
            Vector2 ramDir = (handler.CurrentRamTarget.position - transform.position).normalized;
            currentDest = handler.ClosestPointInsideRange(handler.RamThreshold);
            handler.RamCollider.SetActive(true);
            handler.RB.velocity = new Vector2(ramDir.x * ramSpeed, handler.RB.velocity.y);
            StartedCharging = true;
        }
        if (GameManager.Instance.generalFunctions.IsInRange(transform.position, currentDest, ramTargetOffset) || handler.CheckForFlip() || tookDamage)
        {
            handler.ShroomActor.TakeDamageGFX.RemoveListener(TurnOnTookDamage);
            tookDamage = false;
            handler.Freeze(ramRecoveryTime);
            handler.RamCollider.SetActive(false);
            StartedCharging = false;
            return handler.ShroomIdle;
        }
        return this;       
    }

    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

    private void TurnOnTookDamage()
    {
        tookDamage = true;
    }


    IEnumerator Ram()
    {
        Vector2 ramDir = (GameManager.Instance.assets.playerActor.transform.position - transform.position).normalized;
        currentDest = handler.ClosestPointInsideRange(handler.RamThreshold);
        handler.RamCollider.SetActive(true);
        while (!GameManager.Instance.generalFunctions.IsInRange(transform.position, currentDest, ramTargetOffset) && handler.ShroomGroundCheck.IsEverythingGrounded())
        {
            handler.RB.velocity = new Vector2(ramDir.x * ramSpeed, handler.RB.velocity.y);
            yield return new WaitForEndOfFrame();
        }
        handler.RamCollider.SetActive(false);
        handler.stunned = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, ramTargetOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentDest, 0.5f);
    }

}
