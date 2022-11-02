using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckCollection : MonoBehaviour
{
    //List<GroundCheck> groundChecks = new List<GroundCheck>();

    GroundCheck[] groundChecks;

    public GroundCheck[] GroundChecks { get => groundChecks;}

    public bool CompletleyGrounded => IsEverythingGrounded();

    private void Awake()
    {
        groundChecks = GetComponents<GroundCheck>();
    }

    public bool IsEverythingGrounded()
    {
        foreach (var item in groundChecks)
        {
            if (!item.isGrounded())
            {
                return false;
            }
        }
        return true;
    }

    public bool IsAtLeastOneGrounded()
    {
        bool Grounded = false;
        foreach (var item in groundChecks)
        {
            if (item.isGrounded())
            {
                Grounded = true;
            }
        }

        if (Grounded)
        {
            return true;
        }
        return false;

    }

    public GroundCheck FalseGroundCheck()
    {
        foreach (var item in groundChecks)
        {
            if (!item.isGrounded())
            {
                return item;
            }
        }
        return null;
    }
}
