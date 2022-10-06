using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField]private TextMesh text;
    [SerializeField] float dragSpeed;
    [SerializeField] float dragTime;

    public void SetUp(string givenString, Vector2 position, Vector2 direction)
    {
        text.text = givenString;
        transform.position = position;
        LeanTween.move(gameObject, direction * dragSpeed, dragTime);
    }



    public void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
