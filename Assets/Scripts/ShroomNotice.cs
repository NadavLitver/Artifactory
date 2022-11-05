using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomNotice : State
{
    StoneShroomStateHandler handler;
    [SerializeField] float defenseThreshold;
    [SerializeField] float noticeThreshold;
    public override State RunCurrentState()
    {
        if (handler.AttackMode)
        {
            if (GameManager.Instance.generalFunctions.IsInRange(transform.position, GameManager.Instance.assets.Player.transform.position, defenseThreshold))
            {//if the player is inside the defense zone
                handler.RB.velocity = Vector2.zero;
                return handler.ShroomRam;
            }
            return handler.ShroomLookForCap;
        }
        else
        {
            if (GameManager.Instance.generalFunctions.IsInRange(transform.position, GameManager.Instance.assets.Player.transform.position, defenseThreshold))
            {//if the player is inside the defense zone
                handler.RB.velocity = Vector2.zero;
                return handler.ShroomDefense;
            }
            if (GameManager.Instance.generalFunctions.IsInRange(transform.position, GameManager.Instance.assets.Player.transform.position, noticeThreshold))
            {
                return handler.ShroomThrow;
            }
        }
        return handler.ShroomIdle;
    }

    // Start is called before the first frame update
    private void Start()
    {
        handler = GetComponent<StoneShroomStateHandler>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, defenseThreshold);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, noticeThreshold);

    }

}
