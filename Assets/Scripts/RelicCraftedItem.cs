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
        switch (myRelic.MyEffectEnum)
        {
            case StatusEffectEnum.LightningEmblem:
                sprite = GameManager.Instance.assets.LightningEmblem;
                break;
            case StatusEffectEnum.HealingGoblet:
                sprite = GameManager.Instance.assets.HealingGoblet;
                break;
            default:
                break;
        }
    }
}
