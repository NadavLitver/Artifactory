using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomAnimationEventsD : MonoBehaviour
{
    [SerializeField] ShroomDefenseHandler handler;
    public void FlagThrow()
    {
        handler.throwFlag = true;
    }
}
