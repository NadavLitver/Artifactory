using UnityEngine;
using System;
using System.Collections;
public class SpikedFrogJumpToPlayer : State
{
    SpikedFrogStateHandler handler;
    [SerializeField] LayerMask groundLayer;
    bool enteredState;
    public override State RunCurrentState()
    {
        if (!enteredState)
        {
            SoundManager.Play(SoundManager.Sound.SpikedFromJump, handler.m_audioSource);
            Vector2 rayOriginPoint = new Vector3(GameManager.Instance.assets.Player.transform.position.x, GameManager.Instance.assets.Player.transform.position.y + 10);
            RaycastHit2D hit = Physics2D.Raycast(rayOriginPoint, Vector2.down, groundLayer);
            if (hit && handler.rayData.isPointInBoxButNotInCollider(hit.point))
            {
                handler.launcher.Launch(handler.rayData.GetClosestPointToPoint(hit.point));
                handler.m_animator.Play(handler.FrogJumpAttackHash);
            }
            enteredState = true;
            return this;
        }
        if (handler.launcher.IsJumping)
        {
           
            return this;
        }
        else
        {
            enteredState = false;
            return handler.spikedFrogNotice;

        }
     
        

       
    }

    
    // Start is called before the first frame update
    void Start()
    {
        handler = GetComponent<SpikedFrogStateHandler>();
    }

}
