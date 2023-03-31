using System.Collections;
using UnityEngine;

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
        SoundManager.Play(SoundManager.Sound.BasicGunExplosion, transform.position, SoundManager.GetVolumeOfClip(SoundManager.Sound.BasicGunExplosion));

        rb.velocity = Vector2.zero;
        exploded = true;
        Explosion = GameManager.Instance.assets.ExplsionOP.GetPooledObject();
        if (!ReferenceEquals(Explosion, null))
        {
            Explosion.CacheSource(source);
            Explosion.transform.position = transform.position;
            Explosion.gameObject.SetActive(true);
        }
        //Explosion.gameObject.SetActive(true); place an explosion in this position.
        TurnOff();
    }

    private void TurnOff()
    {
        gameObject.SetActive(false);
        exploded = false;
    }

    IEnumerator ExplodeOnLifeTimeExpired()
    {
        yield return new WaitForSeconds(lifeTime);
        Explode();
    }

}
