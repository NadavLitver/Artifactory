using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedFrogIdle : State
{
    BallLauncher launcher;
    EnemyRayData rayData;
    EnemyLineOfSight lineOfSight;


    [SerializeField] State NoticePlayerState;
    public override State RunCurrentState()
    {
        
    }

    void Start()
    {
        launcher = GetComponent<BallLauncher>();
        rayData = GetComponent<EnemyRayData>();
        lineOfSight = GetComponent<EnemyLineOfSight>();
    }

    
    void Update()
    {
        
    }
}
