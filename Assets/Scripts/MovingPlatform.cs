using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform destenation;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 startingPos;
    [SerializeField] Vector3 currentDestenation;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource m_audioSource;
    Transform playerOriginalParent;
    bool controllingPlayer;
    void Start()
    {
        startingPos = transform.localPosition;
        currentDestenation = startingPos;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (controllingPlayer)
                return;

            GameManager.Instance.inputManager.onJumpDown.AddListener(JumpFromFlower);
            GameManager.Instance.assets.PlayerController.ResetVelocity();
            playerOriginalParent = collision.transform.parent;
            collision.transform.SetParent(transform, true);
            currentDestenation = destenation.localPosition;
            anim.SetTrigger("Move");
            controllingPlayer = true;
            SoundManager.Play(SoundManager.Sound.OnDandilion, m_audioSource);
            GameManager.Instance.assets.PlayerController.canMove = false;
            GameManager.Instance.assets.PlayerController.SetOnDandilion(controllingPlayer);


        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ReleasePlayer();
            GameManager.Instance.inputManager.onJumpDown.RemoveListener(JumpFromFlower);
        }
    }

    private void JumpFromFlower()
    {
        Debug.Log("tried to jump");
        ReleasePlayer();
        GameManager.Instance.assets.PlayerController.ExteriorJump();
    }
    private void ReleasePlayer()
    {
        if (controllingPlayer)
        {
            GameManager.Instance.assets.Player.transform.SetParent(playerOriginalParent, true);
            currentDestenation = startingPos;
            transform.localPosition = startingPos;
            GameManager.Instance.assets.PlayerController.canMove = true;
            controllingPlayer = false;
            GameManager.Instance.assets.PlayerController.SetOnDandilion(controllingPlayer);
        }
    }

    public void MoveTowardsDestenation()
    {
        if (!GameManager.Instance.generalFunctions.IsInRange(currentDestenation, transform.localPosition, 0.1f) && !GameManager.Instance.assets.playerActor.IsInAttackAnim)
        {
            Vector3 direction = (currentDestenation - transform.localPosition).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            ReleasePlayer();
        }
    }

    private void Update()
    {
        MoveTowardsDestenation();
    }

}
