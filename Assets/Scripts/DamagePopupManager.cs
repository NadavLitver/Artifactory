using System.Collections;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [SerializeField] private PopupPool popupPool;
    public void SetDamagePopup(DamageHandler amount, Vector2 pos)
    {
        DamagePopup pop = popupPool.GetPooledObject();
        pop.gameObject.SetActive(true);
        if (amount.myDmgType == DamageType.critical)
        {
            pop.SetUp(amount, pos, GetDirectionToFloatIn(), 1.8f);        }
        else
        {
            pop.SetUp(amount, pos, GetDirectionToFloatIn());
        }
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

    public Color GetDamageTypeColor(DamageType givenType)
    {
        switch (givenType)
        {
            case DamageType.normal:
                return new Color(185, 237, 239);
            case DamageType.heal:
                return Color.green;
            case DamageType.critical:
                return Color.red;

            default:
                return Color.black;
        }
    }

}
