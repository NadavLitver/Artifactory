using System.Collections;
using UnityEngine;

public class ShroomThrowD : BaseShroomDState
{
    [SerializeField] private float throwThreshold;
    [SerializeField] private float throwForce;
    public override IEnumerator StateRoutine()
    {
        yield return new WaitUntil(() => handler.throwFlag);
        handler.throwFlag = false;
        handler.CurrentCap = GameManager.Instance.assets.CapPool.GetPooledObject();
        handler.CurrentCap.gameObject.SetActive(true);
        handler.CurrentCap.transform.position = new Vector3(transform.position.x, transform.position.y + 2);
        handler.CurrentCap.SetUpPositions(handler.Bounder.MaxPos, handler.Bounder.MinPos);
        handler.CurrentCap.Throw(handler.GetPlayerDirection() * throwForce);
        //enter evil mode
        gameObject.SetActive(false);
        //gameobject inactive?
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
