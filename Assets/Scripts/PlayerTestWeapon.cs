using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTestWeapon : Weapon
{
    public int damage;
    public UnityEvent onEnemyHit;
    public Ability m_ability;
    protected override void Attack()
    {
        m_animator.Play("Attack");
        
    }
    protected override void OnWeaponHit(Collider2D collision)
    {
      
        if (collision.CompareTag("Enemy"))
        {
            Actor currentEnemyHit = collision.GetComponent<Actor>();
            if (ReferenceEquals(currentEnemyHit, null))
            {
                currentEnemyHit = collision.GetComponentInChildren<Actor>();
                if(ReferenceEquals(currentEnemyHit, null))
                {
                    currentEnemyHit = collision.GetComponentInParent<Actor>();

                }
            }
            if (!ReferenceEquals(currentEnemyHit, null))
            {
                // currentEnemyHit.TakeDamage(damage);
                currentEnemyHit.GetHit(m_ability);
                onEnemyHit.Invoke();
            }
        }
        
    }
}
