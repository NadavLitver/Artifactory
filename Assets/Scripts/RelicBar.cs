using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicBar : MonoBehaviour
{

    private List<Image> relicImages = new List<Image>();
    public void AddRelic(Relic givenRelic)
    {
        Image image = Instantiate(GameManager.Instance.assets.RelicBarImage, transform);
        image.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(givenRelic);
        relicImages.Add(image);
        Image OwnedRelicImage = Instantiate(GameManager.Instance.assets.OwnedRelicImage, GameManager.Instance.assets.OwnedRelicsPanel);
        OwnedRelicImage.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(givenRelic);
        GameManager.Instance.assets.RelicBarDesc.AddRelic(givenRelic);
        
    }

    public void RemoveRelic(Relic givenRelic)
    {
        foreach (var item in relicImages)
        {
            if (item.sprite == GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(givenRelic))
            {
                relicImages.Remove(item);
                Destroy(item);
                return;
            }
        }
    }

}
