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
}
