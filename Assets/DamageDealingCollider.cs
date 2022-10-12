using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class DamageDealingCollider : MonoBehaviour
{
    public string[] tagsToHit;

    public Ability m_ability;

    public UnityEvent onHit;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool hitSomethingWithCorrectTag = false;
        for (int i = 0; i < tagsToHit.Length; i++)
        {
            if (collision.CompareTag(tagsToHit[i]))
            {
                hitSomethingWithCorrectTag = true;
            }
        }
     
        if (hitSomethingWithCorrectTag)
        {
            Actor currentEnemyHit = collision.GetComponent<Actor>();
            if (ReferenceEquals(currentEnemyHit, null))
            {
                currentEnemyHit = collision.GetComponentInChildren<Actor>();
                if (ReferenceEquals(currentEnemyHit, null))
                {
                    currentEnemyHit = collision.GetComponentInParent<Actor>();

                }
            }
            if (!ReferenceEquals(currentEnemyHit, null))
            {
                // currentEnemyHit.TakeDamage(damage);
              //  currentEnemyHit.GetHit(m_ability);
                onHit.Invoke();
            }
        }
        gameObject.SetActive(false);
    }

}
