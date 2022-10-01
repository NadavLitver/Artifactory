using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicTestWeapon : MonoBehaviour
{
    public float Damage;
    public Ability m_ability;
    PlayerActor playerActorRef => GameManager.Instance.assets.playerActor;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerActorRef.GetHit(m_ability);

        }
    }
}
