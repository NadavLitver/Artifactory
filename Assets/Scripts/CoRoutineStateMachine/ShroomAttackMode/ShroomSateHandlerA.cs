using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomSateHandlerA : ShroomBaseHandler
{
    public bool PickedUp;
    public bool DoneRessurecting;
    public bool startRamming;

    [SerializeField] ShroomBaseStateA pickUpState;
    [SerializeField] ShroomBaseStateA ressurectState;
    public GameObject ramCollider;
    public AudioSource m_audioSource;

    protected override void Start()
    {
        base.Start();
        if (!ReferenceEquals(CurrentCap, null))
        {
            //CurrentCap.OnPickedUp.AddListener(InterruptPickup);
            CurrentCap.GroundCheck.OnGrounded.AddListener(InterruptRessurect);
        }
    }

    

    public void InterruptPickup()
    {
        ResetAnim();
        Interrupt(pickUpState);
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
