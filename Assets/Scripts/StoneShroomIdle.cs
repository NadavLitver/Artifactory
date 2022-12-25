using UnityEngine;
public class StoneShroomIdle : State
{
    StoneShroomStateHandler handler;

    [SerializeField] float idleTime;
    float counter;
    public override State RunCurrentState()
    {
        if(counter < idleTime)
        {
            counter += Time.deltaTime;
            handler.RB.velocity = Vector2.zero;
            return this;
        }
        counter = 0;
        if (handler.ShroomLineOfSight.CanSeePlayer())
        {
            return handler.ShroomNotice;
        }
        return handler.ShroomWalk;
    }

    private void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();

    }
}
