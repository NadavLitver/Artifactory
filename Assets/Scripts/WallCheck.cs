using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public LayerMask GroundLayer;
    [SerializeField] float wallCheckDistance;
    public bool invert;
    public bool IsInfrontWall()
    {
        return Physics2D.Raycast(transform.position,transform.localScale.x * (invert? Vector3.left : Vector3.right), wallCheckDistance, GroundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsInfrontWall() ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, transform.localScale.x * (invert ? Vector3.left : Vector3.right) * wallCheckDistance);
    }
}
