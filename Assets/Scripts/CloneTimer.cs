using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CloneTimer : MonoBehaviour
{
    private float MinuteCounter;
    [SerializeField] private TextMeshProUGUI m_text;
    int hours;
    int minutes;
    private string m_Time;
    private void Start()
    { 

        hours =3;
        minutes = 47;
        MinuteCounter = 60;
    }
    private void Update()
    {
        MinuteCounter -= Time.deltaTime;
        if(MinuteCounter < 0)
        {
            minutes --;
            MinuteCounter = 60;
            if(minutes < 0)
            {
                hours -= 1;
                minutes = 60;
                if(hours < 0)
                {
                    hours = 4;
                }
            }
        }
        m_Time = hours + "h" + "  " + minutes + "m"  + "  " + (int)MinuteCounter + "s";

        m_text.text = "Next Clone: " + m_Time;

    }
}
