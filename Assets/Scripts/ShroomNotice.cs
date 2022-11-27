public class ShroomNotice : State
{
    StoneShroomStateHandler handler;
    public override State RunCurrentState()
    {
        handler.Anim.SetTrigger("Notice");
        if (handler.AttackMode)
        {
            if (GameManager.Instance.generalFunctions.CalcRange(transform.position, GameManager.Instance.assets.playerActor.transform.position) <= GameManager.Instance.generalFunctions.CalcRange(transform.position, handler.CurrentCap.transform.position))
            {
                handler.CurrentRamTarget = GameManager.Instance.assets.Player.transform;
            }
            handler.CurrentRamTarget = handler.CurrentCap.transform;
            handler.Anim.SetTrigger(handler.Ramhash);
            return handler.ShroomRam;
        }
        else
        {
            if (handler.isPlayerWithinDefenseRange())
            {
                handler.Anim.SetTrigger(handler.Defendhash);
                return handler.ShroomDefense;
            }
            else if (handler.isPlayerWithinThrowRange() && handler.Enraged)
            {
                handler.Anim.SetTrigger(handler.Throwhash);

                return handler.ShroomThrow;
            }
            else if (!handler.Enraged)
            {
                handler.Anim.SetTrigger(handler.Idlehash);

                return handler.ShroomMoveBackwards;
            }
        }
        handler.Anim.SetTrigger(handler.Idlehash);
        return handler.ShroomIdle;
    }

    // Start is called before the first frame update
    private void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }



}
