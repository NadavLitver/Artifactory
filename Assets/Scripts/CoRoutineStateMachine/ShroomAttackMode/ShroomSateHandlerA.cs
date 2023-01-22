using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSateHandlerA : ShroomBaseHandler
{
    public bool PickedUp;
    public bool DoneRessurecting;
    public bool DoneDying;
    public bool startRamming;

    [SerializeField] ShroomBaseStateA pickUpState;
    [SerializeField] ShroomBaseStateA deathState;
    [SerializeField] ShroomBaseStateA ressurectState;
    public GameObject ramCollider;

    protected override void Start()
    {
        base.Start();
        Actor.OnDeath.AddListener(AdressDeath);
        if (!ReferenceEquals(CurrentCap, null))
        {
            CurrentCap.OnPickedUp.AddListener(InterruptPickup);
        }
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
        OnDeath?.Invoke();
        ResetAnim();
        Interrupt(deathState);
    }
    public void InterruptRessurect()
    {
        ResetAnim();
        Interrupt(ressurectState);
    }

    public override void ResetAnim()
    {
        base.ResetAnim();
        PickedUp = false;
        startRamming = false;
        DoneRessurecting = false;
        DoneDying = false;
    }
}
