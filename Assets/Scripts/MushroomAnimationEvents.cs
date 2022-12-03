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
    public void OnFinishDie()
    {
        if (handler.AttackMode)
        {
            handler.StartRessurect();
        }
        else 
        {
            handler.RB.gameObject.SetActive(false);
        }
    }
    public void OnFinishRes()
    {
        handler.ReturnToIdle();
        handler.AddTakeDamageListenerBack();
    }
}
