using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupParticaleSystemHandler : MonoBehaviour
{
   [SerializeField] ParticleSystem m_particale;
    ParticleSystem.MainModule m_particaleMainMoudle;
    private void Awake()
    {
        m_particaleMainMoudle = m_particale.main;
        //m_particale.sizeOverLifetime.enabled;
    }
    private void OnParticleSystemStopped()
    {
        m_particale.Stop();
    }
    
}
