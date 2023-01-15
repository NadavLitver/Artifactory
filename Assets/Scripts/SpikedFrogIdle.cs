using UnityEngine;

public class SpikedFrogIdle : State
{
   [SerializeField] SpikedFrogStateHandler handler;
    void Start()
    {
        if (ReferenceEquals(handler, null))
        {
          handler = GetComponent<SpikedFrogStateHandler>();
        }

        //handler.launcher.JumpedEvent.AddListener(TurnOffLanded);
    }
    public override State RunCurrentState()
    {
       
        if (!handler.launcher.IsJumping)  
        {
            handler.launcher.Launch(handler.rayData.GetRandomPos());
            handler.m_animator.Play(handler.FrogIdleHash);
        }

        if (handler.lineOfSight.CanSeePlayer())
        {
            return handler.spikedFrogNotice;
        }

        return this;
    }



   
}
