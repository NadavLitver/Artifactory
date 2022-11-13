using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;


public class FPSCounter : MonoBehaviour
{
    public int avgFrameRate;
    public TextMeshProUGUI display_Text;

    private void Awake()
    {
        Application.targetFrameRate = 300;
    }

    public void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        display_Text.text = avgFrameRate.ToString() + " FPS";
    }
}
