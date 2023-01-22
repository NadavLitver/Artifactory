using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInteractablePanelHandler : MonoBehaviour
{
    public void ReturnToBase()
    {
        GameManager.Instance.assets.blackFade.FadeToBlack();
        LeanTween.delayedCall(1f, PutPlayerInBase);
        LeanTween.delayedCall(2f, GameManager.Instance.assets.blackFade.FadeFromBlack);
        GameManager.Instance.assets.baseFatherObject.SetActive(true);
        GameManager.Instance.LevelManager.ClearRooms();
        this.gameObject.SetActive(false);
    }
    public void PutPlayerInBase()
    {
        GameManager.Instance.assets.Player.transform.position = GameManager.Instance.assets.baseSpawnPlayerPositionObject.transform.position;
    }
}
