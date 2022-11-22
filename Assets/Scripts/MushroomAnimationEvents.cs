using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAnimationEvents : MonoBehaviour
{
    [SerializeField] StoneShroomStateHandler handler;

    public void ThrowCap()
    {
        ShroomCap cap = handler.GetCapToThrow();
        cap.transform.position = transform.position;
        cap.SetUpPositions(handler.Bounder.MaxPos, handler.Bounder.MinPos);
        cap.gameObject.SetActive(true);
        cap.Throw(new Vector2(handler.GetPlayerDirection().x * handler.ThrowForce, 0));
        handler.Freeze(handler.ThrowDelay);
    }
}
