using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableHUDButton : MonoBehaviour
{
    [SerializeField] Button m_button;
    bool triggered;
    private void Start()
    {
        triggered = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered)
            return;
        if (collision.CompareTag("Player"))
        {
            m_button.gameObject.SetActive(true);
            triggered = true;
        }
    }
}
