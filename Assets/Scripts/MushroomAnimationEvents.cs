using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAnimationEvents : MonoBehaviour
{
    [SerializeField] StoneShroomStateHandler handler;

    public void ThrowCap()
    {
        handler.ReadyToThrow = true;
    }
}
