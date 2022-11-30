using UnityEngine;

public class ShroomRam : State
{
    [SerializeField] float ramSpeed;
    [SerializeField] float ramTargetOffset;
    [SerializeField] float ramRecoveryTime;
    StoneShroomStateHandler handler;
    Vector3 currentDest;
    [SerializeField] bool StartedCharging;
    bool tookDamage;


    public override void onStateEnter()
    {
        handler.Anim.SetTrigger(handler.Ramhash);
        handler.ShroomActor.TakeDamageGFX.AddListener(TurnOnTookDamage);
        currentDest = handler.ClosestPointInsideRange(handler.RamThreshold);
        Vector2 ramDir = (currentDest - transform.position).normalized;
        handler.RamCollider.SetActive(true);
        handler.RB.velocity = new Vector2(ramDir.x * ramSpeed, handler.RB.velocity.y);
        StartedCharging = true;
    }
    public override State RunCurrentState()
    {
       
        if (GameManager.Instance.generalFunctions.IsInRange(transform.position, currentDest, ramTargetOffset) || handler.CheckForFlip() || tookDamage)
        {
            StopCharge();
            return handler.ShroomWalk;
        }
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, ramTargetOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentDest, 0.5f);
    }

}
