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
    float threw;

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
        threw = Time.time;
        RB.constraints = RigidbodyConstraints2D.None;
        RB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //calc direction
            Vector2 forceDir = GameManager.Instance.assets.playerActor.transform.position - transform.position;
            ShroomCapAbility.CacheForceDirection(forceDir);
            GameManager.Instance.assets.playerActor.GetHit(ShroomCapAbility);
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time - threw >= pickupCd)
        {
            StoneShroomStateHandler handler = collision.GetComponentInChildren<StoneShroomStateHandler>();
            if (!ReferenceEquals(handler, null))
            {
                handler.AttackMode = false;
                gameObject.SetActive(false);
            }
        }
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
        RB.constraints = RigidbodyConstraints2D.FreezeAll;
        if (RB.velocity.x > 0)
        {
            transform.position = maxPoint;
        }
        else
        {
            transform.position = minPoint;
        }
    }
}
