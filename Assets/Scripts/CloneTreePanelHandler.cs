using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneTreePanelHandler : MonoBehaviour
{
    [SerializeField] Image cloneImage;
    [SerializeField] GameObject BaseUI;
    [SerializeField] GameObject MapPanel;

    bool routineLive;
    public void OnYes()
    {
        //start generation
        //spawn player in the first rooms starting position
        // GameManager.Instance.LevelManager.AssembleLevel();
        StartCoroutine(OnYesRoutine());
    }
    public void OnNo()
    {
        GameManager.Instance.assets.mobileControls.SetActive(true);

    }
    IEnumerator OnYesRoutine()
    {
        if (routineLive)
            yield break;

        routineLive = true;
        Color goal = new Color(1, 1, 1, 1);
        Color StartingColor = new Color(1, 1, 1, 0);
        float counter = 0;
        Time.timeScale = 0;
        SoundManager.Play(SoundManager.Sound.RoomPortalSound, GameManager.Instance.LevelManager.audioSource);
        while(cloneImage.color.a < 1)
        {
            cloneImage.color = Color.Lerp(StartingColor, goal, counter);
            counter += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;

        GameManager.Instance.assets.choiceWorldHandler.gameObject.SetActive(true);
        MapPanel.SetActive(true);
        GameManager.Instance.assets.mobileControls.SetActive(false);
        BaseUI.SetActive(false);
        gameObject.SetActive(false);
        routineLive = false;
         
    }
    private void OnDisable()
    {
        cloneImage.color = new Color(1, 1, 1, 0);
    }
}
