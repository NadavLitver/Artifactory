using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Ability impactAbility;
    [SerializeField] float lifeTime;
    [SerializeField] float Speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject Explosion;
    bool exploded = false;
    [SerializeField] bool explosive;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.right * Speed * GameManager.Instance.assets.Player.transform.localScale.x;
        LeanTween.delayedCall(lifeTime, Explode);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //the impact of the bullet itself.
        Actor actor = collision.gameObject.GetComponent<Actor>();
        if (!ReferenceEquals(actor, null))
        {
            actor.GetHit(impactAbility);
            Explode();
        }
    }

    private void Explode()
    {
        if (exploded || !explosive)
        {
            return;
        }
        rb.velocity = Vector2.zero;
        exploded = true;
        Explosion.SetActive(true);
        LeanTween.delayedCall(1f, TurnOff);
    }

    private void TurnOff()
    {
        Explosion.SetActive(false);
        gameObject.SetActive(false);
        exploded = false;
    }


}
