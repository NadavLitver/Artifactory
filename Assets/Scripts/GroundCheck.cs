using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask GroundLayer;
    [SerializeField] float groundCheckDistance;
    [SerializeField] Vector3 offset;
    public bool isGrounded()
    {
        return Physics2D.Raycast(transform.position + offset, Vector2.down, groundCheckDistance, GroundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded() ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + offset, Vector2.down * groundCheckDistance);
    }
}
