using UnityEngine;

public class ShroomThrow : State
{
    [SerializeField] float throwForce;
    [SerializeField] float throwDelay;
    StoneShroomStateHandler handler;

    public override State RunCurrentState()
    {
        /* handler.Freeze(throwDelay);
         ShroomCap cap = handler.GetCapToThrow();
         cap.transform.position = transform.position;
         cap.SetUpPositions(handler.Bounder.MaxPos, handler.Bounder.MinPos);
         cap.gameObject.SetActive(true);
         cap.Throw(new Vector2(handler.GetPlayerDirection().x * throwForce, 0));
         handler.AttackMode = true;
         handler.Freeze(throwDelay);*/

        handler.AttackMode = true;
        if (ReferenceEquals(handler.CurrentCap, null))
        {
            return this;
        }
        Debug.Log("threw cap");
        handler.Anim.SetTrigger(handler.Noticehash);
        return handler.ShroomNotice;
    }

    // Start is called before the first frame update
    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

}
