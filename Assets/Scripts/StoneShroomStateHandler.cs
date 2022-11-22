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
    public Animator Anim;

    public float DefenseThreshold;
    public float ThrowThreshold;
    public float RamThreshold;
    public int MovementDir;
    public bool RequireFlip;
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
        if (!frozen && !stunned && Bounder.Done)
        {
            RunStateMachine();
        }
    }

    private void Start()
    {
        ShroomActor.TakeDamageGFX.AddListener(Enrage);
        ShroomActor.OnDeath.AddListener(Freeze);
        ShroomActor.OnDeath.AddListener(RespawnOnCap);
    }
    public void RespawnOnCap()
    {
        //play repsawn anim here i guess
        if (!AttackMode)
        {
            return;
        }
        gameObject.SetActive(true);
        transform.parent.position = new Vector2(CurrentCap.transform.position.x, CurrentCap.transform.position.y);
        ShroomActor.HealBackToFull();
        Freeze(0.5f);
        Interrupt(ShroomIdle);
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

    public void Freeze()
    {
        frozen = true;
        RB.constraints = RigidbodyConstraints2D.FreezePosition;
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

    public bool CheckForFlip()
    {
        if (GameManager.Instance.generalFunctions.IsInRange(transform.position, Bounder.MaxPos, 1.5f) && RB.velocity.x > 0)
        {
            return true;
        }
        else if (GameManager.Instance.generalFunctions.IsInRange(transform.position, Bounder.MinPos, 1.5f) && RB.velocity.x < 0)
        {
            return true;
        }
        return false;
    }

    public Vector2 GetPlayerDirection()
    {
        return (GameManager.Instance.assets.playerActor.transform.position - transform.position).normalized;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DefenseThreshold);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RamThreshold);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ThrowThreshold);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 1);

    }


}
