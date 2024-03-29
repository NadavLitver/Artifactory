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
    private int JumpTriggerHash;
    private bool playerIn;
    private Animator m_animator;
    [SerializeField] AudioSource m_audioSource;
    private void Start()
    {
        dir = new Vector2(x, y);
        playerControllerRef = GameManager.Instance.assets.Player.GetComponent<PlayerController>();
        m_animator = GetComponentInChildren<Animator>();
        JumpTriggerHash = Animator.StringToHash("Trigger");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerControllerRef.transform.position.y - transform.position.y > 1 && playerControllerRef.GetIsGrounded && !playerControllerRef.GetIsJumping)
        {
            playerIn = true;
            m_animator.SetTrigger(JumpTriggerHash);
           
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIn = false;

         
        }
    }
    private void GivePlayerForce()//animation event
    {
        if (playerIn)
        {
            playerControllerRef.ResetVelocity();
            playerControllerRef.RecieveForce(dir * force);
            SoundManager.Play(SoundManager.Sound.OnJumpPlatform, m_audioSource);
        }
     
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, GizmoDir * force);
    }

    
}

