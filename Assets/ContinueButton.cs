using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    
    void Start()
    {
        if (SaveLoadSystem.noSaveFile)
        {
            this.gameObject.SetActive(false);
        }
    }

    
        
    
}
