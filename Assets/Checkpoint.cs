using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CheckpointManager.LastCheckPointPosition = transform.position;
            if (!CheckpointManager.hasPosition)
            {
             CheckpointManager.hasPosition = true;
            }
        }
    }
}
