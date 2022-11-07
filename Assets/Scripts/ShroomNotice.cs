using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomNotice : State
{
    StoneShroomStateHandler handler;
    public override State RunCurrentState()
    {
        if (handler.AttackMode)
        {
            if (GameManager.Instance.generalFunctions.CalcRange(transform.position, GameManager.Instance.assets.playerActor.transform.position) <= GameManager.Instance.generalFunctions.CalcRange(transform.position, handler.CurrentCap.transform.position) )
            {
                handler.CurrentRamTarget = GameManager.Instance.assets.Player.transform;   
            }
            handler.CurrentRamTarget = handler.CurrentCap.transform;
            return handler.ShroomRam;
        }
        else
        {
            if (handler.isPlayerWithinDefenseRange())
            {
                return handler.ShroomDefense;
            }
            else if (handler.isPlayerWithinThrowRange() && handler.Enraged)
            {
                return handler.ShroomThrow;
            }
            else if (!handler.Enraged)
            {
                return handler.ShroomMoveBackwards;
            }

        }
        return handler.ShroomIdle;
    }

    // Start is called before the first frame update
    private void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

   

}
