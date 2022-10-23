using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] protected Ability enemyAbility;
    [SerializeField] EnemyActor host;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnWeaponHit(other);
    }


    protected virtual void OnWeaponHit(Collider2D givenCollider)
    {
        if (givenCollider.gameObject.CompareTag("Player"))
        {
            Actor actorHit = givenCollider.GetComponent<Actor>();
            if (!ReferenceEquals(actorHit, null))
            {
                actorHit.GetHit(enemyAbility, host);
            }
        }
    }

}
