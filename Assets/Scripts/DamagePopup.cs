using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField]private TextMesh text;


    public void SetUp(string givenString, Vector2 position)
    {
        text.text = givenString;
        transform.position = position;
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
