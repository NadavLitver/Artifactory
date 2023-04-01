using UnityEngine;

public class SpikedFrogNoticePlayer : State
{
    SpikedFrogStateHandler handler;
    public override State RunCurrentState()
    {
        if (handler.launcher.IsJumping)
        {
            return this;
        }
        handler.m_animator.Play(handler.FrogNoticeHash);
        PlayIdleStopSound();
        handler.Freeze(0.5f);
        if (handler.rayData.isPointInBoxButNotInCollider(GameManager.Instance.assets.Player.transform.position))
        {   //enemy is in range to jump
            return handler.SpikedFrogJumpToPlayer;
        }
        else
        {
            //enemy is not in range
            return handler.SpikedFrogChase;
        }
    }



    void Start()
    {
        handler = GetComponent<SpikedFrogStateHandler>();
    }

    public void PlayIdleStopSound()
    {

        if (Random.Range(0, 2) == 1)
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
    }
}
