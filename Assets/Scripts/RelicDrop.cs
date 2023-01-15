using UnityEngine;


public class RelicDrop : DragToPlayer
{
    [SerializeField] Relic myRelic;
    public Relic MyRelic { get => myRelic; }
    [SerializeField] SpriteRenderer m_spriteRenderer;

    public override void OnDragEnd()
    {
        GameManager.Instance.assets.playerActor.PlayerRelicInventory.AddRelic(MyRelic);
        gameObject.SetActive(false);
    }

    public void CacheRelic(Relic givenRelic)
    {
        myRelic = givenRelic;
        m_spriteRenderer.sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(myRelic);
    }
}
