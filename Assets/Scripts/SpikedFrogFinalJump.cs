using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedFrogFinalJump : State
{

    SpikedFrogStateHandler handler;
    public override State RunCurrentState()
    {
        if (!handler.launcher.IsJumping)
        {
            handler.launcher.Launch(handler.rayData.GetClosestPointToPoint(GameManager.Instance.assets.playerActor.transform.position));
            Debug.Log("final jumped");
        }
        else
        {
            return this;
        }
        return handler.spikedFrogIdle;
    }

    // Start is called before the first frame update
    void Start()
    {
        handler = GetComponent<SpikedFrogStateHandler>();
    }

   
}
