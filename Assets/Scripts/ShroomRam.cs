using UnityEngine;

public class ShroomRam : State
{
    [SerializeField] float ramSpeed;
    [SerializeField] float ramTargetOffset;
    [SerializeField] float ramRecoveryTime;
    StoneShroomStateHandler handler;
    Vector3 currentDest;
    bool StartedCharging;
    bool tookDamage;
    [SerializeField] float ramTime;
    [SerializeField] float ramCD;
    private float lastRammed;
    float ramTimeCounter;




    public override void onStateEnter()
    {
        if (StartedCharging || Time.time - lastRammed < ramCD)
        {
            return;
        }
        handler.Anim.SetTrigger(handler.Ramhash);
        handler.ShroomActor.TakeDamageGFX.AddListener(TurnOnTookDamage);
        currentDest = handler.ClosestPointInsideRange(handler.RamThreshold);
        Vector2 ramDir = (currentDest - transform.position).normalized;
        handler.RamCollider.SetActive(true);
        handler.RB.velocity = new Vector2(ramDir.x * ramSpeed, handler.RB.velocity.y);
        StartedCharging = true;
        ramTimeCounter = ramTime;
    }
    public override State RunCurrentState()
    {
        if (handler.CheckForFlip() || tookDamage || ramTimeCounter <= 0)
        {
            StopCharge();
            StartedCharging = false;
            tookDamage = false;
            lastRammed = Time.time;
            handler.ShroomActor.TakeDamageGFX.RemoveListener(TurnOnTookDamage);
            return handler.ShroomWalk;
        }
        ramTimeCounter -= Time.deltaTime;
        return this;
    }

    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

    private void TurnOnTookDamage()
    {
        tookDamage = true;
    }


    private void StopCharge()
    {
        handler.ShroomActor.TakeDamageGFX.RemoveListener(TurnOnTookDamage);
        tookDamage = false;
        //   handler.Freeze(ramRecoveryTime);
        handler.RamCollider.SetActive(false);
        StartedCharging = false;
        ramTimeCounter = ramTime;
    }
    public override void onStateExit()
    {
        StopCharge();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, ramTargetOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentDest, 0.5f);
    }

}
