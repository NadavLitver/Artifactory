using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicBarDescImage : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI Title;
    public Relic RefRelic;

    public void SetUp(Relic givenRelic)
    {
        Image.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(givenRelic);
        Title.text = givenRelic.name;
        RefRelic = givenRelic;
        Text.text = givenRelic.Summery;
    }

}
