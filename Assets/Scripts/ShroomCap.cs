using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ShroomCap : Actor, IDamagable
{
    public Rigidbody2D RB;
    public Ability ShroomCapAbility;
    bool landed;
    [SerializeField] Vector2 maxPoint;
    [SerializeField] Vector2 minPoint;
    [SerializeField] float pickupCd;
    bool dealtDamage;
    float lastThrown;
    float startingGrav;
    public bool Destroyed;

    [SerializeField] SensorGroup groundCheck;

    public UnityEvent OnPickedUp;
    public SensorGroup GroundCheck { get => groundCheck; }
    public float PickupCd { get => pickupCd; set => pickupCd = value; }
    public bool DealtDamage { get => dealtDamage; set => dealtDamage = value; }
    public float LastThrown { get => lastThrown; set => lastThrown = value; }
    public float StartingGrav { get => startingGrav; set => startingGrav = value; }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        effectable = false;
        OnDeath.AddListener(() => gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        lastThrown = Time.time;
        startingGrav = RB.gravityScale;
        Heal(new DamageHandler() { amount = maxHP });
        Destroyed = false;
        OnDeath.AddListener(DestroyedOn);
    }

    private void OnDisable()
    {
        dealtDamage = false;
        RB.gravityScale = startingGrav;
    }

    public void SetUpPositions(Vector2 max, Vector2 min)
    {
        maxPoint = max;
        minPoint = min;
    }


    public void Throw(Vector3 calculatedForce)
    {
        RB.AddForce(calculatedForce, ForceMode2D.Impulse);
        StartCoroutine(KeepToBounries());
        groundCheck.OnGrounded.AddListener(LandedReset);
    }

    private void LandedReset()
    {
        dealtDamage = true;
        RB.velocity = Vector2.zero;
        RB.gravityScale = 0;
        groundCheck.OnGrounded.RemoveListener(LandedReset);
    }
    
    public void DestroyedOn()
    {
        Destroyed = true;
    }

    IEnumerator KeepToBounries()
    {
        yield return new WaitUntil(() => transform.position.x > maxPoint.x || transform.position.x < minPoint.x);
        if (RB.velocity.x > 0)
        {
            transform.position = new Vector2(maxPoint.x - 1, maxPoint.y);
        }
        else
        {
            transform.position = new Vector2(minPoint.x + 1, minPoint.y);
        }
        RB.velocity = Vector2.zero;
        RB.gravityScale = 0;
    }
}
