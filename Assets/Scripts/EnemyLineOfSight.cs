using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    [SerializeField] LayerMask targetLayers;
    public float range;
    [SerializeField] Transform eyes;
    public bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(eyes.position, Vector2.right * transform.parent.localScale.x, range, targetLayers);
        if (!ReferenceEquals(hit.collider, null) && hit.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = CanSeePlayer() ? Color.green : Color.red;
        Gizmos.DrawRay(eyes.position, Vector2.right * transform.parent.localScale.x * range);
    }
}
