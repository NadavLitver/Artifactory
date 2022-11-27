using UnityEngine;


public class RelicDrop : DragToPlayer
{
    [SerializeField] Relic myRelic;
    public Relic MyRelic { get => myRelic; }

    public override void OnDragEnd()
    {
        GameManager.Instance.assets.playerActor.PlayerRelicInventory.AddRelic(MyRelic);
        gameObject.SetActive(false);
    }


}
