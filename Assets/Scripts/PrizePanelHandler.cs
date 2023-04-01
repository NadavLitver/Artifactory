using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SpecialPrizes
{
    BranchAndGlimmering,
    TuffLegs,
    Healing,
}
public class PrizePanelHandler : MonoBehaviour
{
    [SerializeField] AudioSource m_audioSource;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI DescriptionText;

    [SerializeField] Image BlackFadeForPrize;
    [SerializeField] Image ImageForPrize;
    [SerializeField] GameObject UIHolder;
    [SerializeField] RelicPrizeData[] RelicPrizeDatas;
    [SerializeField] ResourcePrizeData[] ResourcePrizeDatas;
    [SerializeField] SpecialPrizeData[] specialPrizeDatas;

    RelicPrizeData currentRelicPrizeData;
    ResourcePrizeData currentResourcePrizeData;
    SpecialPrizeData currentSpecialPrizeData;


    float ImageSizeGoal;
    Vector2 startingSize;
    public bool isDisabled;
    private void Start()
    {
        startingSize = ImageForPrize.rectTransform.sizeDelta;
        ResetCurrentAndPanel();
        
    }
    public void CallShowPrizeFromRelic(Relic relic) => StartCoroutine(OnWinShowPrize(relic));
    public IEnumerator OnWinShowPrize(Relic relic)
    {
        if (isDisabled)
            yield break;
        UIHolder.SetActive(true);
       // SoundManager.Play(SoundManager.Sound.PrizePanelOpen, m_audioSource);
        Time.timeScale = 0;

        currentRelicPrizeData = GetCurrentPrizeDataBasedOnRelic(relic);
        ImageSizeGoal = ImageForPrize.rectTransform.sizeDelta.x * 3;//goal for size
        SetUI(relic);
        // lerp data
        Vector2 vectorGoal = new Vector2(ImageSizeGoal, ImageSizeGoal);
        
        float counter = 0;
        while (counter < 1)
        {

            ImageForPrize.rectTransform.sizeDelta = Vector2.Lerp(startingSize, vectorGoal, counter);

            counter += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;

        ResetCurrentAndPanel();
        // LeanTween.size(ImageForPrize.rectTransform, vectorGoal, 3).setOnComplete(ResetCurrentAndPanel);




    }
    public void CallShowPrizeFromResource(ItemPickup resource) => StartCoroutine(OnWinShowPrize(resource));
    public IEnumerator OnWinShowPrize(ItemPickup resource)
    {
        if (isDisabled)
            yield break;
        UIHolder.SetActive(true);
       // SoundManager.Play(SoundManager.Sound.PrizePanelOpen, m_audioSource);
        Time.timeScale = 0;

        currentResourcePrizeData = GetCurrentPrizeDataBasedOnResource(resource);
        ImageSizeGoal = ImageForPrize.rectTransform.sizeDelta.x * 3;//goal for size
        SetUI(resource);
        // lerp data
        Vector2 vectorGoal = new Vector2(ImageSizeGoal, ImageSizeGoal);

        float counter = 0;
        while (counter < 1)
        {

            ImageForPrize.rectTransform.sizeDelta = Vector2.Lerp(startingSize, vectorGoal, counter);

            counter += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;

        ResetCurrentAndPanel();
        // LeanTween.size(ImageForPrize.rectTransform, vectorGoal, 3).setOnComplete(ResetCurrentAndPanel);




    }
    public void CallShowPrizeFromSpecialPrize(SpecialPrizes prize) => StartCoroutine(OnWinShowPrize(prize));
    public IEnumerator OnWinShowPrize(SpecialPrizes prize)
    {
        if (isDisabled)
            yield break;
        UIHolder.SetActive(true);
        SoundManager.Play(SoundManager.Sound.PrizePanelOpen, m_audioSource);
        Time.timeScale = 0;

        currentSpecialPrizeData = GetCurrentPrizeDataBasedOnSpecialPrize(prize);
        ImageSizeGoal = ImageForPrize.rectTransform.sizeDelta.x * 3;//goal for size
        SetUI(currentSpecialPrizeData);
        // lerp data
        Vector2 vectorGoal = new Vector2(ImageSizeGoal, ImageSizeGoal);

        float counter = 0;
        while (counter < 1)
        {

            ImageForPrize.rectTransform.sizeDelta = Vector2.Lerp(startingSize, vectorGoal, counter);

            counter += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;

        ResetCurrentAndPanel();
        // LeanTween.size(ImageForPrize.rectTransform, vectorGoal, 3).setOnComplete(ResetCurrentAndPanel);




    }
    private void SetUI(Relic relic)
    {
        BlackFadeForPrize.color = new Color(0, 0, 0, 0.75f);//give blackbackground with slight opacity
        ImageForPrize.color = Color.white;

        ImageForPrize.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(relic);
        nameText.text = currentRelicPrizeData.m_name;
        DescriptionText.text = currentRelicPrizeData.m_description;
    }
    private void SetUI(SpecialPrizeData prizeData)
    {
        BlackFadeForPrize.color = new Color(0, 0, 0, 0.75f);//give blackbackground with slight opacity
        ImageForPrize.color = Color.white;

        ImageForPrize.sprite = GameManager.Instance.generalFunctions.GetSpriteFromSpecialPrizeType(prizeData.m_enum);
        nameText.text = currentSpecialPrizeData.m_name;
        DescriptionText.text = currentSpecialPrizeData.m_description;
    }
    private void SetUI(ItemPickup item)
    {
        BlackFadeForPrize.color = new Color(0, 0, 0, 0.75f);//give blackbackground with slight opacity
        ImageForPrize.color = Color.white;

        ImageForPrize.sprite = GameManager.Instance.generalFunctions.GetSpriteFromItemType(item.MyItem);
        nameText.text = currentResourcePrizeData.m_name;
        DescriptionText.text = currentResourcePrizeData.m_description;
    }
    public void ResetCurrentAndPanel()
    {
        BlackFadeForPrize.color = new Color(0, 0, 0, 0);
        ImageForPrize.color = new Color(1,1,1,0);

        ImageForPrize.sprite = null;
        nameText.text = string.Empty;
        DescriptionText.text = string.Empty;
        ImageForPrize.rectTransform.sizeDelta = startingSize;
        UIHolder.SetActive(false);

    }
    public RelicPrizeData GetCurrentPrizeDataBasedOnRelic(Relic relic)
    {
        for (int i = 0; i < RelicPrizeDatas.Length; i++)
        {
            if(RelicPrizeDatas[i].m_enum == relic.MyEffectEnum)
            {
                return RelicPrizeDatas[i];
            }
        }
        Debug.Log("RelicPrizeData = NULL");
        return null;
    }
    public ResourcePrizeData GetCurrentPrizeDataBasedOnResource(ItemPickup resource)
    {
        for (int i = 0; i < ResourcePrizeDatas.Length; i++)
        {
            if (ResourcePrizeDatas[i].m_enum == resource.MyItem)
            {
                return ResourcePrizeDatas[i];
            }
        }
        Debug.Log("ResourcePrizeData = NULL");
        return null;
    }
    public SpecialPrizeData GetCurrentPrizeDataBasedOnSpecialPrize(SpecialPrizes currentPrize)
    {
        for (int i = 0; i < ResourcePrizeDatas.Length; i++)
        {
            if (specialPrizeDatas[i].m_enum == currentPrize)
            {
                return specialPrizeDatas[i];
            }
        }
        Debug.Log("SpecialPrizeData = NULL");
        return null;
    }
    private void OnDisable()
    {
        ResetCurrentAndPanel();
    }

}
[System.Serializable]
public class RelicPrizeData
{
    public string m_name;
    public string m_description;
    public StatusEffectEnum m_enum;
}
[System.Serializable]
public class ResourcePrizeData
{
    public string m_name;
    public string m_description;
    public ItemType m_enum;
}
[System.Serializable]
public class SpecialPrizeData
{
    public string m_name;
    public string m_description;
    public SpecialPrizes m_enum;
}

