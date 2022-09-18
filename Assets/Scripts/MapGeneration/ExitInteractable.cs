using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public enum ExitLocationHorizontal
{
    LEFT,
    RIGHT
}
public enum ExitLocationVertical
{
    TOP,
    BOTTOM
}

public class ExitInteractable : MonoBehaviour
{
    [SerializeField] Room myRoom;
    [SerializeField] ExitLocationInfo exitLocation = new ExitLocationInfo();
    [SerializeField] bool occupied;
    [SerializeField] ExitInteractable otherExit;
    public Room MyRoom { get => myRoom; set => myRoom = value; }
    public ExitLocationInfo ExitLocation { get => exitLocation; set => exitLocation = value; }
    public bool Occupied { get => occupied;}

    public bool CanConnectToExit(ExitInteractable givenExit)//are the exits compatible?
    {
        if (givenExit.ExitLocation.HorizontalPos != ExitLocation.HorizontalPos)
        {
            return true;
        }
        return false;
    }

    public void ConnectToExit(ExitInteractable givenExit)
    {
        otherExit = givenExit;
        occupied = true;
    }
}

[System.Serializable]
public class ExitLocationInfo
{
    public ExitLocationHorizontal HorizontalPos;
    public ExitLocationVertical VerticalPos;

    public bool EquateLoc(ExitLocationInfo givenLocation)
    {
        if (HorizontalPos == givenLocation.HorizontalPos && VerticalPos == givenLocation.VerticalPos)
        {
            return true;
        }
        return false;
    }
    
}

