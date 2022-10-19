using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    [SerializeField] LayerMask targetLayers;
    [SerializeField] float range;
    [SerializeField] Transform eyes;
    public bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(eyes.position, Vector2.right * transform.localScale.x, range, targetLayers);
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }
        return false;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = CanSeePlayer() ? Color.green : Color.red;
        Gizmos.DrawRay(eyes.position, Vector2.right * transform.localScale.x);
    }
}
