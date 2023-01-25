using UnityEngine;
using System;
public class SpikedFrogStateHandler : StateHandler
{
    public Animator m_animator;
    public BallLauncher launcher;
    public EnemyLineOfSight lineOfSight;
    public State spikedFrogIdle;
    public State spikedFrogNotice;
    public State SpikedFrogJumpToPlayer;
    public State SpikedFrogChase;
    public State SpikedFrogFinalJump;
    public State SpikedFrogDeath;
    public EnemyRayData rayData;
    public GroundCheck spikedFrogGroundCheck;
    public Rigidbody2D RB;
    bool frozen;
    public EnemyActor actor;
    [SerializeField] float freezeDuration;
    private int jumpingHash;
    internal int FrogJumpAttackHash;
    internal int FrogNoticeHash;
    internal int FrogIdleHash;
    internal int FrogDeathHash;
    private void Start()
    {
        launcher.LandedEvent.AddListener(Freeze);
        actor.OnDeath.AddListener(Die);
        jumpingHash = Animator.StringToHash("Jumping");
        FrogJumpAttackHash = Animator.StringToHash("FrogJumpAttack");
        FrogNoticeHash = Animator.StringToHash("FrogNotice");
        FrogIdleHash = Animator.StringToHash("FrogIdleJump");
        FrogDeathHash = Animator.StringToHash("FrogDeath");
    }

    private void Update()
    {
        if (!frozen)
        {
            RunStateMachine();
        }
        m_animator.SetBool(jumpingHash, launcher.IsJumping);
            
    }

    public void Freeze()
    {
        frozen = true;
        RB.constraints = RigidbodyConstraints2D.FreezePosition;
        LeanTween.delayedCall(freezeDuration, UnFreeze);
    }
    public void Freeze(float freezeDur)
    {
        frozen = true;
        RB.constraints = RigidbodyConstraints2D.FreezePosition;
        LeanTween.delayedCall(freezeDur, UnFreeze);
    }
    public void UnFreeze()
    {
        if (RB == null)
        {
            return;
        }
        RB.constraints = RigidbodyConstraints2D.None;
        RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        frozen = false;
    }

    private void Die()
    {
        actor.RecieveStatusEffects(StatusEffectEnum.Invulnerability);
        Interrupt(SpikedFrogDeath);
        RB.velocity = Vector3.zero;
        RB.gravityScale = 0;
    }

}
