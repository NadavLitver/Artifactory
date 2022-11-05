using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomRam : State
{
    [SerializeField] float ramSpeed;
    [SerializeField] float ramTargetOffset;
    StoneShroomStateHandler handler;
    Vector3 currentDest;
    bool startedRamming;
    public override State RunCurrentState()
    {
        if (!startedRamming)
        {//started
            Vector2 ramDir = (GameManager.Instance.assets.playerActor.transform.position - transform.position).normalized;
            currentDest = GameManager.Instance.assets.playerActor.transform.position;
            handler.RB.velocity = new Vector2(ramDir.x * ramSpeed, handler.RB.velocity.y);
            startedRamming = true;
            handler.RamCollider.SetActive(true);
        }
        if (GameManager.Instance.generalFunctions.IsInRange(transform.position, currentDest, ramTargetOffset))
        {//reached
            startedRamming = false;
            DisableCollider();
            return handler.ShroomIdle;
        }
        return this;

    }

    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

    void DisableCollider()
    {
        handler.RamCollider.SetActive(false);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, ramTargetOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentDest, 1);
    }

}
