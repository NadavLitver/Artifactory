using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShroomStateHandler : StateHandler
{
    public State ShroomIdle;
    public State ShroomDefense;
    public State ShroomMoveBackwards;
    public State ShroomNotice;
    public State ShroomRam;
    public State ShroomThrow;
    public State ShroomLookForCap;
    public EnemyLineOfSight ShroomLineOfSight;
    public GroundCheckCollection ShroomGroundCheck;
    public Rigidbody2D RB;
    public RigidBodyFlip Flipper;
    public bool AttackMode;
    public EnemyActor ShroomActor;
    public ShroomCap ShroomCap;


}
