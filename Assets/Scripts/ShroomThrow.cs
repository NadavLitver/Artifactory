using UnityEngine;

public class ShroomThrow : State
{
    [SerializeField] float throwForce;
    [SerializeField] float throwDelay;
    StoneShroomStateHandler handler;

    public override State RunCurrentState()
    {
        //get a shroom cap from an object pooler
        //set its position to be equal to yours
        //add force to it in the direction of the player
        //return notice
        handler.Anim.SetTrigger("Throw");
        handler.Freeze(throwDelay);
        ShroomCap cap = handler.GetCapToThrow();
        cap.transform.position = transform.position;
        cap.SetUpPositions(handler.Bounder.MaxPos, handler.Bounder.MinPos);
        cap.gameObject.SetActive(true);
        cap.Throw(new Vector2(handler.GetPlayerDirection().x * throwForce, 0));
        handler.AttackMode = true;
        handler.Freeze(throwDelay);
        return handler.ShroomNotice;
    }

    // Start is called before the first frame update
    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

}
