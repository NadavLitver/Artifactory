using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform destenation;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 startingPos;
    [SerializeField] Vector3 currentDestenation;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Animator anim;
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

            playerOriginalParent = collision.transform.parent;
            collision.transform.SetParent(transform, true);
            currentDestenation = destenation.localPosition;
            anim.SetTrigger("Move");
            controllingPlayer = true;
            GameManager.Instance.assets.PlayerController.canMove = false;
            GameManager.Instance.assets.PlayerController.SetOnDandilion(controllingPlayer);


        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ReleasePlayer();

        }
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
        if (!GameManager.Instance.generalFunctions.IsInRange(currentDestenation, transform.localPosition, 0.1f))
        {
            Vector3 direction = (currentDestenation - transform.localPosition).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            ReleasePlayer();
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsDestenation();
    }

}
