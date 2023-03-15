using System.Collections;
using UnityEngine;

public class ShroomThrowD : BaseShroomDState
{
    [SerializeField] private float throwThreshold;
    [SerializeField] private float throwForce;
    public override IEnumerator StateRoutine()
    {
        handler.Rb.velocity = Vector3.zero;
        handler.LookTowardsPlayer();
        handler.Anim.SetTrigger("Throw");
        yield return new WaitUntil(() => handler.throwFlag);
        handler.throwFlag = false;
        handler.CurrentCap = GameManager.Instance.assets.CapPool.GetPooledObject();
        handler.CurrentCap.gameObject.SetActive(true);
        handler.CurrentCap.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f);
        handler.CurrentCap.SetUpPositions(handler.Bounder.MaxPos, handler.Bounder.MinPos);
        handler.CurrentCap.Throw(new Vector3(handler.GetPlayerDirection().x, 0, 0) * throwForce);
        yield return new WaitForEndOfFrame();
        handler.SwitchToOtherMode();
    }

    internal override bool myCondition()
    {
        if (handler.IsPlayerWithinThreshold(throwThreshold))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, throwThreshold);
    }
}
