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
    public override void onStateEnter()
    {
        if(Random.Range(0,4) == 1)
        {
            int frogIdleNum = Random.Range(0, 3);
            switch (frogIdleNum)
            {
                case 0:
                    SoundManager.Play(SoundManager.Sound.FrogIdle1);
                    break;
                case 1:
                    SoundManager.Play(SoundManager.Sound.FrogIdle2);
                    break;
                case 2:
                    SoundManager.Play(SoundManager.Sound.FrogIdle3);
                    break;
                default:
                    break;
            }
        }
        base.onStateEnter();
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
