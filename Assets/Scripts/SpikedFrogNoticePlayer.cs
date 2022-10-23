public class SpikedFrogNoticePlayer : State
{
    SpikedFrogStateHandler handler;
    public override State RunCurrentState()
    {
        if (handler.rayData.isPointInBoxButNotInCollider(GameManager.Instance.assets.Player.transform.position))
        {   //enemy is in range to jump
            return handler.SpikedFrogJumpToPlayer;
        }
        else
        {
            //enemy is not in range
            return handler.SpikedFrogChase;
        }
    }



    void Start()
    {
        handler = GetComponent<SpikedFrogStateHandler>();
    }


}
