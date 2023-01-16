using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomRamA : ShroomBaseStateA
{
    [SerializeField] private float ramCoolDown;
    [SerializeField] private float ramDuration;
    [SerializeField] private float bounderOffest;
    [SerializeField] private float ramSpeed;
    [SerializeField] private float RamThreshold;
    private float lastRammed;

    public override IEnumerator StateRoutine()
    {
        float counter = 0;
        handler.Anim.SetBool("Ram", true);
        handler.ramCollider.SetActive(true);
        while (counter < ramDuration && !handler.IsWithinRangeToBounder(bounderOffest))
        {
            handler.Rb.velocity = new Vector2(handler.GetPlayerDirection().x * ramSpeed, 0);
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        handler.ramCollider.SetActive(false);
        handler.Anim.SetBool("Ram", false);
        handler.Rb.velocity = Vector3.zero;
    }

    private void Start()
    {
        lastRammed = ramCoolDown * -1;
    }

    internal override bool myCondition()
    {
        if (Time.time - lastRammed >= ramCoolDown && handler.IsPlayerWithinThreshold(RamThreshold))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RamThreshold);
    }
}
