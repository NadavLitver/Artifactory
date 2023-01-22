using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInteractablePanelHandler : MonoBehaviour
{
    public void ReturnToBase()
    {
        GameManager.Instance.assets.blackFade.FadeToBlack();
        LeanTween.delayedCall(2f, PutPlayerInBaseAndUnfade);
        GameManager.Instance.assets.baseFatherObject.SetActive(true);

       
        this.gameObject.SetActive(false);
    }
    public void PutPlayerInBaseAndUnfade()
    {
        PutPlayerInBase();
        GameManager.Instance.assets.blackFade.FadeFromBlack();
        GameManager.Instance.LevelManager.ClearRooms();
    }
    public void PutPlayerInBase()
    {
        GameManager.Instance.assets.Player.transform.position = GameManager.Instance.assets.baseSpawnPlayerPositionObject.transform.position;
    }
}
