using UnityEngine;

public class ShroomThrow : State
{
    [SerializeField] float throwForce;
    StoneShroomStateHandler handler;

    public override State RunCurrentState()
    {
        //get a shroom cap from an object pooler
        //set its position to be equal to yours
        //add force to it in the direction of the player
        //return notice
        handler.RB.velocity = Vector2.zero;
        ShroomCap cap = handler.GetCapToThrow();
        cap.transform.position = transform.position;
        cap.gameObject.SetActive(true);
        Vector2 throwDir = (GameManager.Instance.assets.playerActor.transform.position - transform.position).normalized;
        cap.RB.AddForce(new Vector2(throwDir.x * throwForce, 0), ForceMode2D.Impulse);
        handler.AttackMode = true;
        return handler.ShroomNotice;

    }

    // Start is called before the first frame update
    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

}
