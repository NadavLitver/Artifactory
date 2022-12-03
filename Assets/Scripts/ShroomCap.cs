using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShroomCap : MonoBehaviour, IDamagable
{
    public Rigidbody2D RB;
    public Ability ShroomCapAbility;
    bool landed;
    [SerializeField] Vector2 maxPoint;
    [SerializeField] Vector2 minPoint;
    [SerializeField] float pickupCd;
    bool dealtDamage;
    float LastThrown;
    float startingGrav;

    [SerializeField] GroundCheckNew groundCheck;

    public GroundCheckNew GroundCheck { get => groundCheck; }

    private void Update()
    {
        if (groundCheck.IsGrounded())
        {
            dealtDamage = true;
            RB.velocity = Vector2.zero;
            RB.gravityScale = 0;
        }
       
    }

    public void GetHit(Ability m_ability)
    {
        TakeDamage(m_ability.DamageHandler);
    }

    public void GetHit(Ability m_ability, Actor host)
    {
        TakeDamage(m_ability.DamageHandler, host);

    }

    public void Heal(DamageHandler givenDmg)
    {

    }

    public void TakeDamage(DamageHandler dmgHandler)
    {

    }

    public void TakeDamage(DamageHandler dmgHandler, Actor host)
    {

    }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        LastThrown = Time.time;
        startingGrav = RB.gravityScale;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !dealtDamage)
        {
            //calc direction
            Vector2 forceDir = GameManager.Instance.assets.playerActor.transform.position - transform.position;
            ShroomCapAbility.CacheForceDirection(forceDir);
            GameManager.Instance.assets.playerActor.GetHit(ShroomCapAbility);
            dealtDamage = true;
            return;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time - LastThrown >= pickupCd)
        {
            StoneShroomStateHandler handler = collision.GetComponentInChildren<StoneShroomStateHandler>();
            if (!ReferenceEquals(handler, null) && handler.AttackMode)
            {
                Debug.Log("picked up");
                handler.PickupInteruption();
                handler.Anim.SetTrigger(handler.Pickuphash);
                handler.AttackMode = false;
                gameObject.SetActive(false);
            }
        }
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
    }

    IEnumerator KeepToBounries()
    {
        yield return new WaitUntil(() => transform.position.x > maxPoint.x || transform.position.x < minPoint.x);
        Debug.Log("found");
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
