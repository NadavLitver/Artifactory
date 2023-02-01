using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedFrogDeath : State
{
    SpikedFrogStateHandler handler;
    void Start()
    {
        handler = GetComponent<SpikedFrogStateHandler>();
    }

    public override void onStateEnter()
    {
        handler.m_animator.Play(handler.FrogDeathHash);
        SoundManager.Play(SoundManager.Sound.SpikedFrogDeath, handler.m_audioSource);

    }
    public override State RunCurrentState()
    {
        return this;
    }
}
