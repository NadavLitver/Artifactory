using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedFrogNoticePlayer : State
{
    SpikedFrogStateHandler handler;
    public override State RunCurrentState()
    {
        //enlarge eyes/ look at the player stupidley animation? 

       /* if (!GameManager.Instance.assets.PlayerController.GetIsGrounded || !handler.spikedFrogGroundCheck.isGrounded())
        {
            //wait for the player to land
            
            return this; 
        }*/
       
        
            if(handler.rayData.isPointInBoxButNotInCollider(GameManager.Instance.assets.Player.transform.position))
            {   //enemy is in range to jump
                return handler.SpikedFrogJumpToPlayer;
            }
            else
            {
                //enemy is not in range
                 return handler.SpikedFrogChase;
            }
        

        //return handler.spikedFrogIdle;
    }

   

    void Start()
    {
        handler = GetComponent<SpikedFrogStateHandler>();
    }

  
}
