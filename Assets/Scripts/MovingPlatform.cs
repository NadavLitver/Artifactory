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
    void Start()
    {
        startingPos = transform.localPosition;
        currentDestenation = startingPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOriginalParent = collision.transform.parent;
            collision.transform.SetParent(transform, true);
            currentDestenation = destenation.localPosition;
            anim.SetTrigger("Move");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(playerOriginalParent, true);
            currentDestenation = startingPos;
            transform.localPosition = startingPos;
        }
    }

    public void MoveTowardsDestenation()
    {
        if (!GameManager.Instance.generalFunctions.IsInRange(currentDestenation, transform.localPosition, 0.1f))
        {
            Vector3 direction = (currentDestenation - transform.localPosition).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsDestenation();
    }

}
