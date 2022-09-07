using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestActor : Actor
{
    public LayerMask GroundLayer;
    [SerializeField] float groundCheckDistance;
    
    public bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, GroundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded() ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * groundCheckDistance);
    }
}
