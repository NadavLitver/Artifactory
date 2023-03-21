using UnityEngine;

[CreateAssetMenu(fileName = "New Crafted Relic", menuName = "Crafted Relic")]

public class RelicCraftedItem : CraftedItem
{
    [SerializeField] private Relic myRelic;

    public override void Obtain()
    {
        GameManager.Instance.assets.playerActor.PlayerRelicInventory.AddRelic(myRelic);
    }

    public override void SetUp()
    {
        sprite = GameManager.Instance.RelicManager.GetRelicSpriteFromRelic(myRelic);
    }
}
