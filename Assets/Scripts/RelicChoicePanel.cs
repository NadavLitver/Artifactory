using UnityEngine;
using UnityEngine.UI;

public class RelicChoicePanel : MonoBehaviour
{
    [SerializeField] private Image rightImage;
    [SerializeField] private Image leftImage;
    private Relic leftRelic;
    private Relic rightRelic;
    private ChoiceRoomFlower flower;

    public void CacheFlower(ChoiceRoomFlower givenFlower)
    {
        flower = givenFlower;
    }
    public void CacheRightRelic(Relic relic)
    {
        leftRelic = relic;
        rightImage.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(relic);
    }

    public void CacheLeftRelic(Relic relic)
    {
        rightRelic = relic;
        leftImage.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(relic);
    }

    //right button
    public void DropRightRelic()
    {
        RelicDrop drop = Instantiate(GameManager.Instance.assets.relicDropPrefab, transform.position, Quaternion.identity, transform);
        drop.CacheRelic(rightRelic);
        flower.ChoseRelic = true;
        flower.Anim.Play("OpenRight");
        gameObject.SetActive(false);
    }
    

    //left button
    public void DropLeftRelic()
    {
        RelicDrop drop = Instantiate(GameManager.Instance.assets.relicDropPrefab, transform.position, Quaternion.identity, transform);
        drop.CacheRelic(leftRelic);
        flower.ChoseRelic = true;
        flower.Anim.Play("OpenLeft");
        gameObject.SetActive(false);
    }
}
