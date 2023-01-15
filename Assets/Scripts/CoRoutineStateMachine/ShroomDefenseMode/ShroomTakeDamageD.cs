using System.Collections;
using UnityEngine;

public class ShroomTakeDamageD : BaseShroomDState
{
    [SerializeField] private float stunDuration;
    [SerializeField] private float knockBackForce;
    public override IEnumerator StateRoutine()
    {
        handler.Anim.SetBool("TakeDmg", true);
        handler.Rb.velocity = Vector3.zero;
        Vector2 dir = (GameManager.Instance.assets.playerActor.transform.position - transform.position).normalized;
        handler.Rb.AddForce(dir * knockBackForce * -1, ForceMode2D.Impulse);
        yield return new WaitForSecondsRealtime(stunDuration);
        handler.Anim.SetBool("TakeDmg", false);
    }


    internal override bool myCondition()
    {
        return true;
    }
}
