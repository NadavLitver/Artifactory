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
    public EnemyRayData rayData;
    public GroundCheck spikedFrogGroundCheck;
    public Rigidbody2D RB;
    bool frozen;
    [SerializeField] float freezeDuration;

    private void Start()
    {
        launcher.LandedEvent.AddListener(Freeze);
    }

    private void Update()
    {
        if (!frozen)
        {
            RunStateMachine();
        }
        m_animator.SetBool("Jumping", launcher.IsJumping);
            
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


}
