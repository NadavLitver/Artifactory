using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonHandler : MonoBehaviour
{
    private Button m_button;
    private PauseMenuHandler PauseHandler =>  GameManager.Instance.pauseMenuHandler;
    public GameObject PauseMenuPanel;
    void Start()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(OnPause);
    }
    public void OnPause()
    {
        if (PauseHandler.isPaused)
        {
            PauseHandler.UnPause();
            PauseMenuPanel.SetActive(false);
        }
        else
        {
            PauseHandler.Pause();
            PauseMenuPanel.SetActive(true);

        }

    }

}
