using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomCapHitCollider : MonoBehaviour
{
    [SerializeField] ShroomCap cap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !cap.DealtDamage)
        {
            //calc direction
            SoundManager.Play(SoundManager.Sound.MushroomEnemyCapHitPlayer, cap.m_audioSource);

           // Vector2 forceDir = GameManager.Instance.assets.playerActor.transform.position - transform.position;
            GameManager.Instance.assets.playerActor.GetHit(cap.ShroomCapAbility);
            return;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time - cap.LastThrown >= cap.PickupCd)
        {
            ShroomBaseHandler handler = collision.GetComponentInChildren<ShroomBaseHandler>();
            if (!ReferenceEquals(handler, null))
            {
                transform.parent.gameObject.SetActive(false);
                cap.OnPickedUp?.Invoke();
            }
        }
    }


}
