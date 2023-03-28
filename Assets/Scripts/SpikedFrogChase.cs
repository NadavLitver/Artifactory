using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedFrogChase : State
{
    SpikedFrogStateHandler handler;
    public override State RunCurrentState()
    {
        if (!handler.launcher.IsJumping)
        {
            handler.launcher.Launch(handler.rayData.GetClosestPointToPoint(GameManager.Instance.assets.playerActor.transform.position));
           // Debug.Log("chased jumped");
        }
        else
        {
            return this;
        }

        //if the player is in range return jump to player
        if (handler.rayData.isPointInBoxButNotInCollider(GameManager.Instance.assets.playerActor.transform.position))
        {
            return handler.SpikedFrogJumpToPlayer;
        }
        else 
        {
            return handler.SpikedFrogFinalJump;
        }
        //if the player is not in range jump once to the point thats closest to the player and return notice 
       
    }

    void Start()
    {
        handler = GetComponent<SpikedFrogStateHandler>();
    }

    
}
