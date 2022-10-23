using UnityEngine;

public class SpikedFrogIdle : State
{
    SpikedFrogStateHandler handler;
    public override State RunCurrentState()
    {
        //play jump anim?
        if (!handler.launcher.IsJumping)
        {
            handler.launcher.Launch(handler.rayData.GetRandomPos());
            Debug.Log("idle jumped");
         }

        if (handler.lineOfSight.CanSeePlayer())
        {
            return handler.spikedFrogNotice;
        }

        return this;
    }


 
    void Start()
    {
        handler = GetComponent<SpikedFrogStateHandler>();
     
       //handler.launcher.JumpedEvent.AddListener(TurnOffLanded);
    }
}
