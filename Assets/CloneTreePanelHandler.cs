using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneTreePanelHandler : MonoBehaviour
{
    public void StartRun()
    {
        //start generation
        //spawn player in the first rooms starting position
        GameManager.Instance.LevelManager.AssembleLevel();
        gameObject.SetActive(false);
    }
}
