using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpawner : MonoBehaviour
{
    Actor owner;

    private void Start()
    {
        owner = GetComponent<Actor>();
        owner.OnDamageCalcOver.AddListener(SpawnDmgPopup);
        //added to the attacked actor 
        owner.OnRecieveHealth.AddListener(SpawnDmgPopup);
    }

    private void SpawnDmgPopup(DamageHandler givendmg)
    {
        GameManager.Instance.PopupManager.SetDamagePopup(givendmg, transform.position);
    }
}

  
