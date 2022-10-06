using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
   [SerializeField] private PopupPool popupPool;
    public void SetDamagePopup(string amount, Vector2 pos)
    {
        DamagePopup pop = popupPool.GetPooledObject();
        pop.gameObject.SetActive(true);
        pop.SetUp(amount, pos);
        StartCoroutine(turnoff(pop.gameObject));
    }

    IEnumerator turnoff(GameObject pop)
    {
        yield return new WaitForSecondsRealtime(1f);
        pop.SetActive(false);
    }
    
}
