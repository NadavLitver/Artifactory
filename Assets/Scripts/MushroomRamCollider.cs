using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomRamCollider : MonoBehaviour
{
    Actor owner;
    [SerializeField] Ability ability;
    bool dealtDamage;
    private void Start()
    {
        owner = GetComponentInParent<Actor>();
    }
    private void OnEnable()
    {
        dealtDamage = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !dealtDamage)
        {
            Actor actor = collision.GetComponent<Actor>();
            actor.GetHit(ability, owner);
            dealtDamage = true;
        }
    }
}
