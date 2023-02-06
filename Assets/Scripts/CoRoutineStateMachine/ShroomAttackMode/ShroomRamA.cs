using System;
using System.Collections;
using UnityEngine;

public class ShroomRamA : ShroomBaseStateA
{
    [SerializeField] private float ramDuration;
    [SerializeField] private float bounderOffest;
    [SerializeField] private float ramSpeed;


    public override IEnumerator StateRoutine()
    {
        float counter = 0;
        handler.Anim.SetTrigger("StartRam");
        handler.Anim.SetBool("Ram", true);
        handler.Flipper.Disabled = false;
        handler.Rb.velocity = Vector3.zero;
        yield return new WaitUntil(() => handler.startRamming);
        handler.startRamming = false;
        handler.Actor.OnDealDamage.AddListener(OnRamHit);
        handler.ramCollider.SetActive(true);
        Vector2 dir = (handler.CurrentCap.transform.position - transform.position).normalized;
        dir *= ramSpeed;
        while (counter < ramDuration && !handler.IsWithinRangeToBounder(bounderOffest))
        {
            handler.Rb.velocity = dir;
            counter += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        handler.Actor.OnDealDamage.RemoveListener(OnRamHit);
        Debug.Log("done ramming");
        handler.ramCollider.SetActive(false);
        handler.Anim.SetBool("Ram", false);
        handler.Rb.velocity = Vector3.zero;
    }

    private void OnRamHit(DamageHandler _dmgHandler, Actor _actor)
    {
        SoundManager.Play(SoundManager.Sound.MushroomEnemyRamHit, handler.m_audioSource);
    }

    internal override bool myCondition()
    {
        return true;
    }

   
}
