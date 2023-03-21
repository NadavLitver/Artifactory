using System.Collections;
using UnityEngine;

public class TextTriggerOnStay : MonoBehaviour
{
    [SerializeField] Transform WorldPosForTextBox;
    [SerializeField] RectTransform RelatedTextBox;
    private bool playerInArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RelatedTextBox.gameObject.SetActive(true);
        playerInArea = true;
        StartCoroutine(KeepTextBoxInPos());
         
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    RelatedTextBox.position = Camera.main.WorldToScreenPoint(WorldPosForTextBox.position);
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        RelatedTextBox.gameObject.SetActive(false);
        playerInArea = false;
    }
    IEnumerator KeepTextBoxInPos()
    {
        RelatedTextBox.gameObject.SetActive(true);
        while (playerInArea)
        {
            RelatedTextBox.position = Camera.main.WorldToScreenPoint(WorldPosForTextBox.position);
            yield return new WaitForEndOfFrame();
        }
        RelatedTextBox.gameObject.SetActive(false);
    }
}
