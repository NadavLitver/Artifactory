using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatRoomUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private float duration;

    public IEnumerator SetTitle(string title)
    {
        this.title.text = title;
        yield return new WaitForSecondsRealtime(duration);
        gameObject.SetActive(false);
    }





}
