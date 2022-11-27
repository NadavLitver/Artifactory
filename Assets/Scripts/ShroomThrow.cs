using UnityEngine;

public class ShroomThrow : State
{
    StoneShroomStateHandler handler;

    public override State RunCurrentState()
    {
        //play throw animation
        handler.AttackMode = true;
        if (!handler.ReadyToThrow)
        {
            return this;
        }

        ShroomCap cap = handler.GetCapToThrow();
        cap.transform.position = transform.position;
        cap.SetUpPositions(handler.Bounder.MaxPos, handler.Bounder.MinPos);
        cap.gameObject.SetActive(true);
        cap.Throw(new Vector2(handler.GetPlayerDirection().x * handler.ThrowForce, 0));
        handler.Freeze(handler.ThrowDelay);
        handler.ReadyToThrow = false;

        return handler.ShroomNotice;
    }

    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

}
