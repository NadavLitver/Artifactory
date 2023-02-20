using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneTreePanelHandler : MonoBehaviour
{
    public void OnYes()
    {
        //start generation
        //spawn player in the first rooms starting position
       // GameManager.Instance.LevelManager.AssembleLevel();
        GameManager.Instance.assets.choiceWorldHandler.gameObject.SetActive(true);
        GameManager.Instance.assets.mobileControls.SetActive(false);

        gameObject.SetActive(false);
    }
    public void OnNo()
    {
        GameManager.Instance.assets.mobileControls.SetActive(true);

    }
}
