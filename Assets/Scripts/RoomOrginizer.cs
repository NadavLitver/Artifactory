using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Room))]
public class RoomOrginizer : MonoBehaviour
{

    [SerializeField, Header("Room hierarchy - left")] private Transform leftFar;
    [SerializeField] private Transform leftMiddle;
    [SerializeField] private Transform leftClose;
    [SerializeField] private Transform leftGround;
    [SerializeField] private Transform leftFore;

    [SerializeField, Header("Room hierarchy - middle")] private Transform middleFar;
    [SerializeField] private Transform middleMiddle;
    [SerializeField] private Transform middleClose;
    [SerializeField] private Transform middleGround;
    [SerializeField] private Transform middleFore;

    [SerializeField, Header("Room hierarchy - right")] private Transform rightFar;
    [SerializeField] private Transform rightMiddle;
    [SerializeField] private Transform rightClose;
    [SerializeField] private Transform rightGround;
    [SerializeField] private Transform rightFore;

    [Header("pointers")]
    [SerializeField] private Transform rightPointer;
    [SerializeField] private Transform leftPointer;

   
    private void Awake()
    {
        SortRoom();
    }




    private void SortRoom()
    {
        SpriteRenderer[] foundRenderers = GetComponentsInChildren<SpriteRenderer>();
        List<SpriteRenderer> rightR = new List<SpriteRenderer>();
        List<SpriteRenderer> leftR = new List<SpriteRenderer>();
        List<SpriteRenderer> middleR = new List<SpriteRenderer>();


        foreach (var item in foundRenderers)
        {
            if (item.transform.position.x >= rightPointer.position.x)
            {
                rightR.Add(item);
            }
            else if (item.transform.position.x <= leftPointer.transform.position.x)
            {
                leftR.Add(item);
            }
            else if (item.transform.position.x < leftPointer.transform.position.x && item.transform.position.x > leftPointer.transform.position.x)
            {
                middleR.Add(item);
            }
        }


        foreach (var item in rightR)
        {
            switch (item.sortingLayerName)
            {
                case "Background":
                    item.transform.SetParent(rightFar);
                    break;
                case "Ground":
                    item.transform.SetParent(rightGround);
                    break;
                case "Middle":
                    item.transform.SetParent(rightMiddle);
                    break;
                case "Close":
                    item.transform.SetParent(rightClose);
                    break;
                case "Foreground":
                    item.transform.SetParent(rightFore);
                    break;
                default:
                    break;
            }
        }
        foreach (var item in leftR)
        {
            switch (item.sortingLayerName)
            {
                case "Background":
                    item.transform.SetParent(leftFar);
                    break;
                case "Ground":
                    item.transform.SetParent(leftGround);
                    break;
                case "Middle":
                    item.transform.SetParent(leftMiddle);
                    break;
                case "Close":
                    item.transform.SetParent(leftClose);
                    break;
                case "Foreground":
                    item.transform.SetParent(leftFore);
                    break;
                default:
                    break;
            }
        }
        foreach (var item in middleR)
        {
            switch (item.sortingLayerName)
            {
                case "Background":
                    item.transform.SetParent(middleFar);
                    break;
                case "Ground":
                    item.transform.SetParent(middleGround);
                    break;
                case "Middle":
                    item.transform.SetParent(middleMiddle);
                    break;
                case "Close":
                    item.transform.SetParent(middleClose);
                    break;
                case "Foreground":
                    item.transform.SetParent(middleClose);
                    break;
                default:
                    break;
            }
        }

    }


}
