using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceWorldManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI titleText;
    public TMPro.TextMeshProUGUI DescriptionText;
    public TMPro.TextMeshProUGUI ResourceText;
    public Image islandImage;
    public Button ChooseButton;
    public Button RightArrow;
    public Button LeftArrow;
    public Image[] ResourceImages;
    private int index;
    [SerializeField] private GameObject lockImage;

    [SerializeField] List<FloatingIslandInChoicePanel> islandsInChoicePanel;

    private void Start()
    {
        index = 0;
        SetPanel(islandsInChoicePanel[index]);
        RightArrow.onClick.AddListener(OnRightArrowClicked);
        LeftArrow.onClick.AddListener(OnLeftArrowClicked);
        ChooseButton.onClick.AddListener(OnClickChooseWorld);

    }

    private void OnClickChooseWorld()
    {
        if (ChooseButton.enabled)
        {
            GameManager.Instance.assets.blackFade.CallFadeToAndFrom();
            GameManager.Instance.LevelManager.AssembleLevel();
            this.gameObject.SetActive(false);
            GameManager.Instance.assets.mobileControls.SetActive(true);

        }
    }

    private void OnLeftArrowClicked()
    {
        if (index - 1 < 0)
        {
            index = islandsInChoicePanel.Count - 1;
            SetPanel(islandsInChoicePanel[index]);
            return;
        }
        else
        {
            index--;
            SetPanel(islandsInChoicePanel[index]);
        }
    }

    private void OnRightArrowClicked()
    {
        if (islandsInChoicePanel.Count - 1 < index + 1)
        {
            index = 0;
            SetPanel(islandsInChoicePanel[index]);
            return;
        }
        else
        {
            index++;
            SetPanel(islandsInChoicePanel[index]);
        }
    }

    public void SetPanel(FloatingIslandInChoicePanel currentIsland)
    {
        islandImage.sprite = currentIsland.m_Sprite;
        titleText.text = currentIsland.titleText;
        DescriptionText.text = currentIsland.DescriptionText;
        ChooseButton.enabled = !currentIsland.isLocked;

        if (currentIsland.isLocked)
        {
            lockImage.SetActive(true);
        }
        else
        {
            lockImage.SetActive(false);

        }
        //foreach (Image img in ResourceImages)
        //{
        //    img.gameObject.SetActive(currentIsland.ShowResources);
        //}
        ResourceText.gameObject.SetActive(currentIsland.ShowResources);
    }
}
[System.Serializable]
public class FloatingIslandInChoicePanel
{
    public Sprite m_Sprite;
    public string titleText;
    public string DescriptionText;
    public bool isLocked;
    public bool ShowResources;


}
