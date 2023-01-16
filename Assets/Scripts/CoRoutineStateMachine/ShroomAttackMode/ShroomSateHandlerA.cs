using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSateHandlerA : ShroomBaseHandler
{
    public bool PickedUp;
    public bool DoneRessurecting;
    public bool DoneDying;

    [SerializeField] ShroomBaseStateA pickUpState;
    [SerializeField] ShroomBaseStateA deathState;
    [SerializeField] ShroomBaseStateA ressurectState;
    public GameObject ramCollider;

    protected override void Start()
    {
        base.Start();
        Actor.OnDeath.AddListener(AdressDeath);
    }

    private void AdressDeath()
    {
        if (CurrentCap.Destroyed)
        {
            InterruptDeath();
        }
        else
        {
            InterruptRessurect();
        }
    }

    public void InterruptPickup()
    {
        ResetAnim();
        Interrupt(pickUpState);
    }
    public void InterruptDeath()
    {
        ResetAnim();
        Interrupt(deathState);
    }
    public void InterruptRessurect()
    {
        ResetAnim();
        Interrupt(ressurectState);
    }
}
