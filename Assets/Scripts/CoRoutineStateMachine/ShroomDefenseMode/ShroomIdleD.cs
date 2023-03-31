using System.Collections;
using UnityEngine;

public class ShroomIdleD : BaseShroomDState
{
    [SerializeField] private float stopDuration;
    [SerializeField] private float idleCoolDown;
    private float lastStopped;
    
    public override IEnumerator StateRoutine()
    {
        lastStopped = Time.time;
        handler.Rb.velocity = Vector2.zero;
        if(Random.Range(0,5) == 1)
          SoundManager.Play(SoundManager.Sound.MushroomIdle, handler.m_audioSource);

        yield return new WaitForSeconds(stopDuration);
    }

    internal override bool myCondition()
    {
        if (Time.time - lastStopped >= idleCoolDown)
        {
            return true;
        }
        return false;
    }

    private void OnEnable()
    {
        lastStopped = idleCoolDown * -1;
    }
}
