using System.Collections;
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
        handler.Anim.SetTrigger("StartRam");
        handler.Anim.SetBool("Ram", true);
        handler.Flipper.Disabled = false;
        yield return new WaitUntil(() => handler.startRamming);
        handler.startRamming = false;
        handler.ramCollider.SetActive(true);
        Vector2 dir = new Vector2(handler.GetPlayerDirection().x * ramSpeed, 0);
        while (counter < ramDuration && !handler.IsWithinRangeToBounder(bounderOffest))
        {
            handler.Rb.velocity = dir;
            counter += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("done ramming");
        handler.ramCollider.SetActive(false);
        handler.Anim.SetBool("Ram", false);
        handler.Rb.velocity = Vector3.zero;
        lastRammed = Time.time;
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
