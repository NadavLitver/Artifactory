using UnityEngine;

public class ShroomBaseHandler : CoRoutineStateHandler
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SensorGroup lineOfSight;
    [SerializeField] private SensorGroup groundCheck;
    [SerializeField] private EnemyBounder bounder;
    [SerializeField] private Animator anim;
    private ShroomCap currentCap;
    public Rigidbody2D Rb { get => rb; }
    public SensorGroup LineOfSight { get => lineOfSight; }
    public SensorGroup GroundCheck { get => groundCheck; }
    public EnemyBounder Bounder { get => bounder; }
    public Animator Anim { get => anim; }
    public ShroomCap CurrentCap { get => currentCap; set => currentCap = value; }

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
}
