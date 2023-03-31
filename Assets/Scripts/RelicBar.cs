using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicBar : MonoBehaviour
{
    public void AddRelic(Relic givenRelic)
    {
        Image image = Instantiate(GameManager.Instance.assets.RelicBarImage, transform);
        image.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(givenRelic);
    }
}
