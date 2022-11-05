using UnityEngine;

public class ShroomMoveBackwards : State
{
    StoneShroomStateHandler handler;

    public override State RunCurrentState()
    {
        handler.Flipper.enabled = false;
        if (handler.ShroomGroundCheck.IsEverythingGrounded())
        {
            handler.RB.velocity = new Vector2(transform.position.x - GameManager.Instance.assets.Player.transform.position.x, handler.RB.velocity.y);
        }

        return handler.ShroomDefense;
        //return defense 
    }


    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }


}
