using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventSoundCalls : MonoBehaviour
{
    [SerializeField] BasicSword swordRef;


    [SerializeField] AudioSource m_audioSource;
    private void Start()
    {
        swordRef.onHit.AddListener(CallSwordHitRandom);
    }
    public void CallPlayerJump() => CallSound(SoundManager.Sound.PlayerJump);
    public void CallPlayerLand() => CallSound(SoundManager.Sound.PlayerLand);
    public void CallPlayerStep1() => CallSound(SoundManager.Sound.PlayerRun1);
    public void CallPlayerStep2() => CallSound(SoundManager.Sound.PlayerRun2);
    public void CallGunShoot1() => CallSound(SoundManager.Sound.BasicGunShoot);
    public void CallGunShoot2() => CallSound(SoundManager.Sound.BasicGunShoot2);
    public void CallGunExplosion() => CallSound(SoundManager.Sound.BasicGunExplosion);
    public void CallGunMobility() => CallSound(SoundManager.Sound.BasicGunMobility);
    public void CallGunCharged() => CallSound(SoundManager.Sound.BasicGunCharged);
    public void CallSwordHit1() => CallSound(SoundManager.Sound.BasicSwordHit1);
    public void CallSwordHit2() => CallSound(SoundManager.Sound.BasicSwordHit2);
    public void CallSwordHit3() => CallSound(SoundManager.Sound.BasicSwordHit3);
    public void CallSwordSwing1() => CallSound(SoundManager.Sound.BasicSwordSwing1);
    public void CallSwordSwing2() => CallSound(SoundManager.Sound.BasicSwordSwing2);
    public void CallSwordSwing3() => CallSound(SoundManager.Sound.BasicSwordSwing3);
    public void CallSwordDash() => CallSound(SoundManager.Sound.BasicSwordDash);

    public void CallSwordHitRandom()
    {
        float value = Random.value;
        if(value > 0.7f)
        {
            CallSwordHit1();
        }
        else if(value > 0.4f)
        {
            CallSwordHit2();

        }
        else if(value < 0.4f)
        {
            CallSwordHit3();

        }
    }


    /* BasicGunShoot,
        BasicGunShoot2,
        BasicGunCharged,
        BasicGunExplosion,
        BasicGunMobility,  */

    public void CallSound(SoundManager.Sound current)
    {
        SoundManager.Play(current, m_audioSource);
    }
}
