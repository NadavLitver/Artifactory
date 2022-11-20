using System.Collections;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class SpikeCollider : MonoBehaviour
{
    [SerializeField] Ability m_ability;
    BlackFade blackFadeRef;
    private bool playerHit;
    private void Start()
    {
        blackFadeRef = GameManager.Instance.assets.blackFade;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playerHit)
            {
                playerHit = true;
                StartCoroutine(OnPlayerHit());
            }
        }
    }
    IEnumerator OnPlayerHit()
    {
        GameManager.Instance.assets.playerActor.GetHit(m_ability);
        GameManager.Instance.assets.PlayerController.canMove = false;
        GameManager.Instance.assets.PlayerController.ResetVelocity();
      
        blackFadeRef.FadeToBlack();
        yield return new WaitForSeconds(2);
        blackFadeRef.FadeFromBlack();

        if (CheckpointManager.hasPosition)
        {
            GameManager.Instance.assets.playerActor.transform.position = CheckpointManager.LastCheckPointPosition;
        }
        else
        {//just in case
            GameManager.Instance.assets.playerActor.transform.position = GameManager.Instance.LevelManager.Active.transform.position;
        }

        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.assets.PlayerController.canMove = true;
        playerHit = false;
    }
}
