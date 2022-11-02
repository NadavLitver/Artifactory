using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomThrow : State
{
    [SerializeField] float throwForce;
    StoneShroomStateHandler handler;

    public override State RunCurrentState()
    {
        Vector2 throwDir = new Vector2(handler.RB.velocity.normalized.x, 0);
        handler.RB.velocity = Vector2.zero;
        handler.ShroomCap.gameObject.SetActive(true);
        handler.ShroomActor.transform.parent = transform.parent.parent;
        handler.ShroomCap.RB.AddForce(throwDir * throwForce, ForceMode2D.Impulse);
        handler.AttackMode = true;
        return handler.ShroomLookForCap;
    }

    // Start is called before the first frame update
    void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

}
