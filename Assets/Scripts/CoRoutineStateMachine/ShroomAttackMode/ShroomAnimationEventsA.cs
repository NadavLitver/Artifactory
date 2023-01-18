using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomAnimationEventsA : MonoBehaviour
{
    [SerializeField] private ShroomSateHandlerA handler;

    public void DoneDying()
    {
        handler.DoneDying = true;
    }

    public void DoneRessurecting()
    {
        handler.DoneRessurecting = true;
    }

    public void StartRamming()
    {
        handler.startRamming = true;
    }

}
