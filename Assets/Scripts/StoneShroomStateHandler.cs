using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StoneShroomStateHandler : StateHandler
{
    public State ShroomIdle;
    public State ShroomDefense;
    public State ShroomNotice;
    public State ShroomRam;
    public State ShroomThrow;
    public State ShroomMoveBackwards;
    public State ShroomWalk;
    public State ShroomPickup;
    public State ShroomThrowWait;
    public State ShroomRessurect;
    public State ShroomDie;
    public State ShroomTakeDmg;


    public EnemyLineOfSight ShroomLineOfSight;
    public GroundCheckCollection ShroomGroundCheck;
    public Rigidbody2D RB;
    public RigidBodyFlip Flipper;
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
    public float ThrowForce;
    public float ThrowDelay;
    public int PickupFreezeDuration;
    public int MovementDir;
    public bool AttackMode;
    public bool RequireFlip;
    public bool Enraged;
    public bool stunned;
    public bool frozen;
    public bool ReadyToThrow;
    public bool capDestroyed;

    internal int Idlehash;
    internal int Throwhash;
    internal int WalkBackhash;
    internal int Ramhash;
    internal int Pickuphash;
    internal int Defendhash;
    internal int WalkChash;
    internal int Walkhash;
    internal int RessurectHash;
    internal int TakeDamageHash;
    internal int TakeDamageHashc;

    internal int Diehash;
    internal int Regenhash;
    public UnityEvent OnPickedUpShroom;
    public UnityEvent OnShroomThrown;
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
        /* else if (frozen)
         {
             if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
             {
                 Anim.Play(Idlehash);
             }

         }*/
    }
    public void PickupInteruption()
    {
        Interrupt(ShroomPickup);
    }
    private void Start()
    {
        ShroomActor.TakeDamageGFX.AddListener(Enrage);
        ShroomActor.OnDamageCalcOver.AddListener(OnHit);
        ShroomActor.TakeDamageGFX.AddListener(TakeDamageInterrupt);
        // ShroomActor.OnDeath.AddListener(Freeze);
        ShroomActor.OnDeath.AddListener(StartDeath);
        OnPickedUpShroom.AddListener(PickupFreeze);


        Idlehash = Animator.StringToHash("Idle");
        Ramhash = Animator.StringToHash("Ram");
        Throwhash = Animator.StringToHash("Throw");
        Pickuphash = Animator.StringToHash("Pickup");
        Defendhash = Animator.StringToHash("Defend");
        WalkChash = Animator.StringToHash("WalkC");
        Walkhash = Animator.StringToHash("Walk");
        WalkBackhash = Animator.StringToHash("WalkBack");
        RessurectHash = Animator.StringToHash("Ressurect");
        TakeDamageHash = Animator.StringToHash("Hit");
        TakeDamageHashc = Animator.StringToHash("HitC");

        Diehash = Animator.StringToHash("Die");
        Regenhash = Animator.StringToHash("Regen");
    }
    public void OnHit(DamageHandler dmgHandler)//1
    {
        if (dmgHandler.calculateFinalNumberMult() < 1 || dmgHandler.calculateFinalNumberMult() >= ShroomActor.currentHP || ShroomActor.currentHP < 1)
            return;
        if (AttackMode)
        {
            Anim.SetTrigger(TakeDamageHash);
        }
        else
        {
            Anim.SetTrigger(TakeDamageHashc);
        }
        Interrupt(ShroomTakeDmg);

    }
    public void TakeDamageInterrupt()//2
    {
        if (ShroomActor.currentHP < 1)
        {
            ShroomActor.OnDamageCalcOver.RemoveListener(OnHit);
        }
    }
    public void StartDeath()
    {
        if (!AttackMode)
        {
            //death called is still called 
            ShroomActor.currentHP = 1;
        }
        else
        {
            Interrupt(ShroomDie);
        }

    }

    public void DestoryCap()
    {
        capDestroyed = true;
    }

    public void AddTakeDamageListenerBack() => ShroomActor.OnDamageCalcOver.AddListener(OnHit);
    public void StartRessurect()
    {
        //play repsawn anim here i guess//no
        if (!AttackMode || capDestroyed)
        {
            return;
        }
        gameObject.SetActive(true);
        transform.parent.position = new Vector2(CurrentCap.transform.position.x, transform.position.y);
        ShroomActor.HealBackToFull();
        ShroomActor.OnDamageCalcOver.AddListener(OnHit);
        Interrupt(ShroomRessurect);

    }
    public void ReturnToIdle()
    {
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


    IEnumerator WaitForAttackMode()
    {
        yield return new WaitUntil(() => AttackMode);
        OnPickedUpShroom?.Invoke();
        StartCoroutine(WaitForNotAttackMode());
    }

    IEnumerator WaitForNotAttackMode()
    {
        yield return new WaitUntil(() => !AttackMode);
        OnShroomThrown?.Invoke();
        StartCoroutine(WaitForAttackMode());
    }

    private void PickupFreeze()
    {
        Freeze(PickupFreezeDuration);
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

