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
        {
            Vector2 ramDirection = new Vector2(GameManager.Instance.assets.playerActor.transform.position.x - transform.position.x, 0).normalized;
            currentDest = GameManager.Instance.assets.Player.transform.position;
            handler.RB.velocity = ramSpeed * ramDirection;
            startedRamming = true;
        }
        if (startedRamming && GameManager.Instance.generalFunctions.IsInRange(transform.position, currentDest, ramTargetOffset))
        {
            return this;
        }
        startedRamming = false;
        return handler.ShroomIdle;
    }

    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, ramTargetOffset);
    }

}
