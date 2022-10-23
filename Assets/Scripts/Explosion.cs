using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] Ability explosionAbility;
    private Weapon source;
    [SerializeField] LayerMask HitLayer;
    private void OnEnable()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, HitLayer);
        foreach (var item in colliders)
        {
            Actor actor = item.GetComponent<Actor>();
            if (!ReferenceEquals(actor, null))
            {
                actor.GetHit(explosionAbility, source.Host);
            }
        }
    }

    public void CacheSource(Weapon givenWeapon)
    {
        source = givenWeapon;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
