using UnityEngine;

public class ShroomMoveBackwards : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float noticeOffset;
    [SerializeField] float moveBackwardsSpeed;
    public override State RunCurrentState()
    {
        handler.Flipper.Disabled = true;
        handler.RB.velocity = new Vector2(transform.parent.localScale.x * -1 * moveBackwardsSpeed, handler.RB.velocity.y);
        if (GameManager.Instance.generalFunctions.IsInRange(GameManager.Instance.assets.playerActor.transform.position, transform.position, handler.ShroomLineOfSight.range + noticeOffset))
        {
            return this;
        }
        handler.ShroomGroundCheck.FlipRequired = true;
        handler.Enrage();
        return handler.ShroomIdle;
    }


    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }


}
