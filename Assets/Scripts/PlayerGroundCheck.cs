using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public bool Grounded;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Grounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Grounded = false;
    }
}
