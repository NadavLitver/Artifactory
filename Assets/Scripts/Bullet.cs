using UnityEngine;
using System;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] Ability impactAbility;
    [SerializeField] float lifeTime;
    [SerializeField] float Speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Explosion Explosion;
    [SerializeField] bool explosive;
    bool exploded = false;

    Weapon source;

    public void CahceSource(Weapon givenWeapon)
    {
        source = givenWeapon;
        if (!ReferenceEquals(Explosion, null))
        {
            Explosion.CacheSource(givenWeapon);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.right * Speed * GameManager.Instance.assets.Player.transform.localScale.x;
        StartCoroutine(ExplodeOnLifeTimeExpired());
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //the impact of the bullet itself.
        Actor actor = collision.gameObject.GetComponent<Actor>();
        if (!ReferenceEquals(actor, null))
        {
            actor.GetHit(impactAbility, source.Host);
        }
        Explode();
    }

    private void Explode()
    {
        if (exploded || !explosive)
        {
            rb.velocity = Vector2.zero;
            exploded = true;
            TurnOff();
            return;
        }
        rb.velocity = Vector2.zero;
        exploded = true;
        Explosion.gameObject.SetActive(true);
        LeanTween.delayedCall(1f, TurnOff);
    }

    private void TurnOff()
    {
        Explosion.gameObject.SetActive(false);
        gameObject.SetActive(false);
        exploded = false;
    }

    IEnumerator ExplodeOnLifeTimeExpired()
    {
        yield return new WaitForSeconds(lifeTime);
        Explode();
    }

}
