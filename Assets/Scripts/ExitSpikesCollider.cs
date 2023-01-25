using UnityEngine;

public class ExitSpikesCollider : MonoBehaviour
{
    [SerializeField] private Ability ability;
    [SerializeField] private float knockBackForce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = GameManager.Instance.generalFunctions.CalcRangeV2(GameManager.Instance.assets.Player.transform.position, transform.position).normalized;
            GameManager.Instance.assets.PlayerController.ResetVelocity();
            GameManager.Instance.assets.PlayerController.RecieveForce(new Vector2(direction.x * knockBackForce, 0));
            Debug.Log(new Vector2(direction.x * knockBackForce, 0));
            //GameManager.Instance.assets.playerActor.GetHit(ability);
        }
    }

}
