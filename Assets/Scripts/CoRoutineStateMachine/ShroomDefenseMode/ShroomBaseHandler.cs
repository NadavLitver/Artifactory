using UnityEngine;
using UnityEngine.Events;

public class ShroomBaseHandler : CoRoutineStateHandler
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SensorGroup lineOfSight;
    [SerializeField] private SensorGroup groundCheck;
    [SerializeField] private EnemyBounder bounder;
    [SerializeField] private Animator anim;
    [SerializeField] private RigidBodyFlip flipper;
    [SerializeField] private CoRoutineState takeDamageState;
    [SerializeField] private ShroomBaseHandler otherMode;
    [SerializeField] CoRoutineState deathState;

    private ShroomCap currentCap;
    private bool capDestroyed;
    public bool DoneDying;
    public UnityEvent OnDeath;
    public Rigidbody2D Rb { get => rb; }
    public SensorGroup LineOfSight { get => lineOfSight; }
    public SensorGroup GroundCheck { get => groundCheck; }
    public EnemyBounder Bounder { get => bounder; }
    public Animator Anim { get => anim; }
    public ShroomCap CurrentCap { get => currentCap; set => currentCap = value; }
    public RigidBodyFlip Flipper { get => flipper; }
    public bool CapDestroyed { get => capDestroyed; set => capDestroyed = value; }

    protected virtual void Start()
    {
        Actor.OnDamageCalcOver.AddListener(InterruptTakeDamage);
        groundCheck.OnGrounded.AddListener(bounder.StartBounder);
        OnDeath.AddListener(Actor.DropResource);
        Actor.OnDeath.AddListener(AdressDeath);

    }

    private void AdressDeath()
    {
        InterruptDeath();
    }

    public void InterruptDeath()
    {
        OnDeath?.Invoke();
        ResetAnim();
        Interrupt(deathState);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ResetAnim();
    }
    public bool IsWithinRangeToBounder(float range)
    {
        if (GameManager.Instance.generalFunctions.CalcRange(transform.position, bounder.MinPos) <= range && rb.velocity.x < 0)
        {
            return true;
        }
        else if (GameManager.Instance.generalFunctions.CalcRange(transform.position, bounder.MaxPos) <= range && rb.velocity.x > 0)
        {
            return true;
        }
        return false;
    }

    public bool IsPlayerWithinThreshold(float threshold)
    {
        if (GameManager.Instance.generalFunctions.CalcRange(transform.position, GameManager.Instance.assets.playerActor.transform.position) <= threshold)
        {
            return true;
        }
        return false;
    }

    public Vector2 GetPlayerDirection()
    {
        return (GameManager.Instance.assets.Player.transform.position - transform.position).normalized;
    }

    public void LookTowardsPlayer()
    {
        if (transform.position.x <= GameManager.Instance.assets.playerActor.transform.position.x)
        {
            flipper.FlipRight();
        }
        else
        {
            flipper.FlipLeft();
        }

    }
    public virtual void InterruptTakeDamage(DamageHandler givenDamage)
    {
        ResetAnim();
        Interrupt(takeDamageState);
    }

    public virtual void ResetAnim()
    {
        foreach (var item in anim.parameters)
        {
            if (item.type == AnimatorControllerParameterType.Bool)
            {
                anim.SetBool(item.name, false);
            }
        }
    }

    public Vector2 GetCurrectCapDirection()
    {
        return (currentCap.transform.position - transform.position).normalized;
    }
    public void SwitchToOtherMode()
    {
        otherMode.currentCap = currentCap;
        otherMode.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
