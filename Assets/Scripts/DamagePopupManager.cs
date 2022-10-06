using System.Collections;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [SerializeField] private PopupPool popupPool;
    public void SetDamagePopup(string amount, Vector2 pos)
    {
        DamagePopup pop = popupPool.GetPooledObject();
        pop.gameObject.SetActive(true);
        pop.SetUp(amount, pos, GetDirectionToFloatIn());
        //StartCoroutine(turnoff(pop.gameObject));
    }

    private Vector2 GetDirectionToFloatIn()
    {
        Vector2 dir = new Vector2(Random.Range(0, 1f), Random.Range(0, 1f)).normalized;
        return dir;
    }

    IEnumerator turnoff(GameObject pop)
    {
        yield return new WaitForSecondsRealtime(1f);
        pop.SetActive(false);
    }

}
