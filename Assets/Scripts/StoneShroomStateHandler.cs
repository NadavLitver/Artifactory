using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShroomStateHandler : StateHandler
{
    public State ShroomIdle;
    public State ShroomDefense;
    public State ShroomNotice;
    public State ShroomRam;
    public State ShroomThrow;
    public State ShroomLookForCap;
    public State ShroomMoveBackwards;

    public EnemyLineOfSight ShroomLineOfSight;
    public GroundCheckCollection ShroomGroundCheck;
    public Rigidbody2D RB;
    public RigidBodyFlip Flipper;
    public bool AttackMode;
    public EnemyActor ShroomActor;
    public ShroomCapObjectPool ShroomCapPool;
    public ShroomCap CurrentCap;
    public GameObject RamCollider;
    public Transform CurrentRamTarget;
    public EnemyBounder Bounder;

    public float DefenseThreshold;
    public float ThrowThreshold;
    public float RamThreshold;
    public int MovementDir;
    public bool Enraged;
    public bool stunned;
    public bool frozen;

    public ShroomCap GetCapToThrow()
    {
        CurrentCap = ShroomCapPool.GetPooledObject();
        return CurrentCap;
    }

    private void Update()
    {
        if (!frozen && !stunned)
        {
            RunStateMachine();
        }
    }

    private void Start()
    {
        ShroomActor.TakeDamageGFX.AddListener(Enrage);
        
    }

    public void Enrage()
    {
        Enraged = true;
        ShroomActor.TakeDamageGFX.RemoveListener(Enrage);

    }
    public void Freeze(float duration)
    {
        frozen = true;
        RB.constraints = RigidbodyConstraints2D.FreezePosition;
        LeanTween.delayedCall(duration, UnFreeze);

    }
    public void UnFreeze()
    {
        RB.constraints = RigidbodyConstraints2D.None;
        RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        frozen = false;
    }

    public void Stun(float duration)
    {
        stunned = true;
        LeanTween.delayedCall(duration, ReleaseStun);

    }
    public void ReleaseStun()
    {
        stunned = false;
    }

    public bool isPlayerWithinDefenseRange()
    {
        if (GameManager.Instance.generalFunctions.IsInRange(GameManager.Instance.assets.playerActor.transform.position, transform.position, DefenseThreshold))
        {
            return true;
        }
        return false;
    }
    public bool isPlayerWithinRamRange()
    {
        if (GameManager.Instance.generalFunctions.IsInRange(GameManager.Instance.assets.playerActor.transform.position, transform.position, RamThreshold))
        {
            return true;
        }
        return false;
    }

    public bool isPlayerWithinThrowRange()
    {
        if (GameManager.Instance.generalFunctions.IsInRange(GameManager.Instance.assets.playerActor.transform.position, transform.position, ThrowThreshold))
        {
            return true;
        }
        return false;
    }

    public Vector2 ClosestPointInsideRange(float range) // returns the closest point on the edge of the range
    {
        Transform playerTrans = GameManager.Instance.assets.playerActor.transform;
        if (playerTrans.position.x > transform.position.x)
        {
            return new Vector2(transform.position.x + range, transform.position.y);
        }
        else
        {
            return new Vector2(transform.position.x - range, transform.position.y);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DefenseThreshold);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RamThreshold);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ThrowThreshold);
    }

   
}
