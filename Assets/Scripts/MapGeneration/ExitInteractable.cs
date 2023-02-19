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
    //[SerializeField] Animator m_animator;

    [SerializeField] Collider2D exitCollider;
    [SerializeField] GameObject spikes;
    [SerializeField] GameObject gfx;
    public Room MyRoom { get => myRoom; set => myRoom = value; }
    public ExitLocationInfo ExitLocation { get => exitLocation; set => exitLocation = value; }
    public bool Occupied { get => occupied;}
    public ExitInteractable OtherExit { get => otherExit; set => otherExit = value; }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {// on remember this is good remove comments!!!
           /* if (!ReferenceEquals(m_animator, null))
            {
                m_animator.SetTrigger("Exited");
            }*/
           GameManager.Instance.LevelManager.MoveToRoom(this);
        }
    }

    public void SetOffExit()
    {
        exitCollider.enabled = false;
        spikes.SetActive(true);
        gfx.SetActive(false);
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

