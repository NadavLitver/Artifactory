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
        rightRelic = relic;
        rightImage.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(relic);
    }

    public void CacheLeftRelic(Relic relic)
    {
        leftRelic = relic;
        leftImage.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(relic);
    }

    //right button
    public void DropRightRelic()
    {
        RelicDrop drop = Instantiate(GameManager.Instance.assets.relicDropPrefab, flower.transform.position, Quaternion.identity);
        drop.CacheRelic(rightRelic);
        flower.ChoseRelic = true;
        flower.Anim.Play("OpenRight");
        gameObject.SetActive(false);
        SoundManager.Play(SoundManager.Sound.ChoiceRelicPicked, flower.m_audioSource);
    }
    

    //left button
    public void DropLeftRelic()
    {
        RelicDrop drop = Instantiate(GameManager.Instance.assets.relicDropPrefab, flower.transform.position, Quaternion.identity);
        drop.CacheRelic(leftRelic);
        flower.ChoseRelic = true;
        flower.Anim.Play("OpenLeft");
        gameObject.SetActive(false);
    }
}
