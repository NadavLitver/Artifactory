using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShroomStateHandler : StateHandler
{
    public State ShroomIdle;
    public State ShroomDefense;
    public State ShroomMoveBackwards;
    public State ShroomNotice;
    public EnemyLineOfSight ShroomLineOfSight;
    public GroundCheckCollection ShroomGroundCheck;
    public Rigidbody2D RB;
    public RigidBodyFlip Flipper;
    public bool AttackMode;
    public EnemyActor ShroomActor;


}
