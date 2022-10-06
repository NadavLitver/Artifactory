using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] Ability explosionAbility;

    private void OnEnable()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var item in colliders)
        {
            Actor actor = item.GetComponent<Actor>();
            if (!ReferenceEquals(actor, null))
            {
                actor.GetHit(explosionAbility);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
