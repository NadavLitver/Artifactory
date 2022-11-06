using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ShroomCap : MonoBehaviour, IDamagable
{
    public Rigidbody2D RB;
    public Ability ShroomCapAbility;
    public GroundCheckCollection groundChecks;
    bool landed;
    
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
        groundChecks = GetComponent<GroundCheckCollection>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
        if (!groundChecks.IsAtLeastOneGrounded())
        {
            return;
        }
        StoneShroomStateHandler handler = collision.GetComponentInChildren<StoneShroomStateHandler>();
        if (!ReferenceEquals(handler, null))
        {
            handler.AttackMode = false;
            gameObject.SetActive(false);
        }
    }

}
