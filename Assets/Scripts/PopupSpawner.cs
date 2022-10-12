using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpawner : MonoBehaviour
{
    Actor owner;

    private void Start()
    {
        owner = GetComponent<Actor>();
        owner.OnDamageCalcOver.AddListener(SpawnPopup);
    }

    private void SpawnPopup(DamageHandler givendmg)
    {
        GameManager.Instance.PopupManager.SetDamagePopup(givendmg, transform.position);
    }  

}
