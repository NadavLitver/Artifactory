using UnityEngine;

public class SpikedFrogStateHandler : StateHandler
{
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
    [SerializeField] float freezeDUration;

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
    }

    public void Freeze()
    {
        frozen = true;
        RB.constraints = RigidbodyConstraints2D.FreezePosition;
        LeanTween.delayedCall(freezeDUration, UnFreeze);
    }
    public void UnFreeze()
    {
        RB.constraints = RigidbodyConstraints2D.None;
        RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        frozen = false;
    }


}
