using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomCapHitCollider : MonoBehaviour
{
    [SerializeField] ShroomCap cap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !cap.GroundCheck.IsGrounded())
        {
            //calc direction
            Vector2 forceDir = GameManager.Instance.assets.playerActor.transform.position - transform.position;
            GameManager.Instance.assets.playerActor.GetHit(cap.ShroomCapAbility);
            return;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time - cap.LastThrown >= cap.PickupCd)
        {
            StoneShroomStateHandler handler = collision.GetComponentInChildren<StoneShroomStateHandler>();
            if (!ReferenceEquals(handler, null) && handler.AttackMode)
            {
                Debug.Log("picked up");
                handler.PickupInteruption();
                handler.Anim.SetTrigger(handler.Pickuphash);
                handler.AttackMode = false;
                transform.parent.gameObject.SetActive(false);
                Debug.Log("shroom picked up cap");
            }
        }
    }


}
