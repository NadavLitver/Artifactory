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
    bool entered;
    public override State RunCurrentState()
    {
        if (!entered)
        {
            entered = true;
            handler.Anim.SetTrigger(handler.Ramhash);
        }
        if (!StartedCharging)
        {
            handler.Anim.SetTrigger("Ram");
            handler.ShroomActor.TakeDamageGFX.AddListener(TurnOnTookDamage);
            Vector2 ramDir = (handler.CurrentRamTarget.position - transform.position).normalized;
            currentDest = handler.ClosestPointInsideRange(handler.RamThreshold);
            handler.RamCollider.SetActive(true);
            handler.RB.velocity = new Vector2(ramDir.x * ramSpeed, handler.RB.velocity.y);
            StartedCharging = true;
        }
        if (GameManager.Instance.generalFunctions.IsInRange(transform.position, currentDest, ramTargetOffset) || handler.CheckForFlip() || tookDamage)
        {
            StopCharge();
            entered = false;
            return handler.ShroomIdle;
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
        handler.Freeze(ramRecoveryTime);
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
