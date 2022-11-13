using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTriggerMoveObject : MonoBehaviour
{
    [SerializeField] Transform Target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LeanTween.move(collision.gameObject, Target.position, 0.5f);
        //collision.transform.position = Target.position;
    }
}
