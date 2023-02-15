using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZooStationGem : MonoBehaviour
{
    [SerializeField] private GameObject greenGem;
    [SerializeField] private GameObject redGem;
    [SerializeField] private GameObject grayGem;

    private void Start()
    {
        SetGrayActive();
    }
    public void SetRedActive()
    {
        greenGem.SetActive(false);
        redGem.SetActive(true);
        grayGem.SetActive(false);
    }
    public void SetGrayActive()
    {
        greenGem.SetActive(false);
        redGem.SetActive(false);
        grayGem.SetActive(true);
    }
    public void SetGreenActive()
    {
        greenGem.SetActive(true);
        redGem.SetActive(true);
        grayGem.SetActive(false);
    }
}
