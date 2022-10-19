using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedFrogNoticePlayer : State
{
    [SerializeField] State jumpToPlayer;
    EnemyRayData rayData;

    public override State RunCurrentState()
    {
        return null;
      /*  if (GameManager.Instance.generalFunctions.IsInRange)
        {

        }*/

    }

    void Start()
    {
        rayData = GetComponent<EnemyRayData>();
    }

  
}
