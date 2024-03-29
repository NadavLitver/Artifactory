using System.Collections;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMesh text;
    [SerializeField] float dragTime;
    [SerializeField, Range(1,3)] float dragDistance;
    [SerializeField, Range(1,2)] float sizeIncrease;
   

    public void SetUp(DamageHandler givenDmg, Vector2 position, Vector2 direction)
    {
        text.color = GameManager.Instance.PopupManager.GetDamageTypeColor(givenDmg.myDmgType);
        text.text = ((int)givenDmg.calculateFinalNumberMult()).ToString();
        transform.position = position;
        LeanTween.move(gameObject, position + (direction * dragDistance), dragTime).setEaseOutCubic();
        LeanTween.scale(gameObject, transform.localScale * sizeIncrease, dragTime);
        // LeanTween.delayedCall(dragTime * 1.3f, TurnOff);// was doing bugs on reset scene maybe can returned later once gameloop is finished
        StartCoroutine(TurnOffRoutine(dragTime * 1.3f));
    }

    
    IEnumerator TurnOffRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        TurnOff();
    }
    public void TurnOff()
    {
       
        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }
    
}
