using UnityEngine;

public class ShroomBaseHandler : CoRoutineStateHandler
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SensorGroup lineOfSight;
    [SerializeField] private SensorGroup groundCheck;
    [SerializeField] private EnemyBounder bounder;
    [SerializeField] private Animator anim;
    [SerializeField] private RigidBodyFlip flipper;
    [SerializeField] private CoRoutineState takeDamageState;
    private ShroomCap currentCap;
    public Rigidbody2D Rb { get => rb; }
    public SensorGroup LineOfSight { get => lineOfSight; }
    public SensorGroup GroundCheck { get => groundCheck; }
    public EnemyBounder Bounder { get => bounder; }
    public Animator Anim { get => anim; }
    public ShroomCap CurrentCap { get => currentCap; set => currentCap = value; }
    public RigidBodyFlip Flipper { get => flipper; }

    private void Start()
    {
        Actor.TakeDamageGFX.AddListener(InterruptTakeDamage);   
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
            flipper.FlipLeft();
        }
        else
        {
            flipper.FlipRight();
        }

    }


    public void InterruptTakeDamage()
    {
        foreach (var item in anim.parameters)
        {
            if (item.type == AnimatorControllerParameterType.Bool)
            {
                anim.SetBool(item.name, false);
            }
        }
        Interrupt(takeDamageState);   
    }
}
