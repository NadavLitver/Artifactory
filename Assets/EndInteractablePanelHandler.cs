using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInteractablePanelHandler : MonoBehaviour
{
    public void ReturnToBase()
    {
        GameManager.Instance.assets.blackFade.FadeToBlack();
        GameManager.Instance.assets.Player.transform.position = GameManager.Instance.assets.baseSpawnPlayerPositionObject.transform.position;
        LeanTween.delayedCall(2f, GameManager.Instance.assets.blackFade.FadeFromBlack);
        this.gameObject.SetActive(false);
    }
}
