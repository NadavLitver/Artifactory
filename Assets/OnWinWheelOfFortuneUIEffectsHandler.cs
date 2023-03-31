using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnWinWheelOfFortuneUIEffectsHandler : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] WheelOfFortunePrizeUIReferenceData[] prizeUIReferenceDatas;
    [SerializeField] Image BlackFadeFromWheelOfFortune;
    WheelOfFortunePrizeUIReferenceData current;
    float ImageSizeGoal;

    public IEnumerator onWinUIGrow(int index)
    {

        current = prizeUIReferenceDatas[index];
        for (int i = 0; i < prizeUIReferenceDatas.Length; i++)
        {
            if (i == index)
            {
                prizeUIReferenceDatas[i].go.SetActive(true);

            }
            else
            {
                prizeUIReferenceDatas[i].go.SetActive(false);

            }
        }
        BlackFadeFromWheelOfFortune.color = new Color(0, 0, 0, 0.75f);
        float counter = 0;
        ImageSizeGoal = current.m_images[0].rectTransform.sizeDelta.x * 3;
        Vector2 goal = new Vector2(ImageSizeGoal, ImageSizeGoal);
        Vector2 startingSize = current.m_images[0].rectTransform.sizeDelta;
        nameText.gameObject.SetActive(true);
        nameText.text = current.name;
        while (counter < 1)
        {
            for (int i = 0; i < current.m_images.Length; i++)
            {
                current.m_images[i].rectTransform.sizeDelta = Vector2.Lerp(startingSize, goal, counter);

            }
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(3);
        BlackFadeFromWheelOfFortune.color = new Color(0, 0, 0, 0);

        nameText.gameObject.SetActive(false);
        CleanCurrent(startingSize);

    }
    public void CleanCurrent(Vector2 startingSize)
    {
        if (ReferenceEquals(current, null))
            return;

        for (int i = 0; i < current.m_images.Length; i++)
        {
            current.m_images[i].rectTransform.sizeDelta = startingSize;

        }
        current.go.SetActive(false);
    }
}
[System.Serializable]
public class WheelOfFortunePrizeUIReferenceData
{
    public string name;
    public GameObject go;
    public Image[] m_images;//ussually one but resources have two
}
