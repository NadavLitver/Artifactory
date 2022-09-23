using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    private PlayerController playerControllerRef;
    [Range(-1,1)] public float x,y;
    public float force;
    private Vector2 GizmoDir => new Vector2(x, y).normalized;

    private Vector2 dir;

    private void Start()
    {
        dir = new Vector2(x, y);
        playerControllerRef = GameManager.Instance.assets.Player.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(IEshootPlayer());
        }
    }
    IEnumerator IEshootPlayer()
    {
        playerControllerRef.canMove = false;
        playerControllerRef.ResetVelocity();
        playerControllerRef.GetRb.AddForce(dir * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        playerControllerRef.canMove = true;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, GizmoDir * force);
    }

    
}

