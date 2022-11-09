using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAreaCamTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //finish this script nadav
        }
    }
}
