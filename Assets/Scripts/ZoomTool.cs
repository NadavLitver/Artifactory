using UnityEngine;

public class ZoomTool : MonoBehaviour
{
    private IZoomAble active;
    private float lastDistance;
    private void Update()
    {
        if (!GameManager.Instance.inputManager.bothFingersDown || ReferenceEquals(active, null))
        {
            if (!ReferenceEquals(active, null))
            {
                active.Scroller().enabled = true;
            }
            return;
        }
        if (GameManager.Instance.generalFunctions.CalcRange(GameManager.Instance.inputManager.Touch_ScreenPos(), GameManager.Instance.inputManager.SecondaryTouch_ScreenPos()) > lastDistance)
        {
            active.Scroller().enabled = false;
            active.ZoomIn();
        }
        else if (GameManager.Instance.generalFunctions.CalcRange(GameManager.Instance.inputManager.Touch_ScreenPos(), GameManager.Instance.inputManager.SecondaryTouch_ScreenPos()) < lastDistance)
        {
            active.Scroller().enabled = false;
            active.ZoomOut();
        }
        lastDistance = GameManager.Instance.generalFunctions.CalcRange(GameManager.Instance.inputManager.Touch_ScreenPos(), GameManager.Instance.inputManager.SecondaryTouch_ScreenPos());
    }


    public void CacheActive(IZoomAble active)
    {
        this.active = active;
    }

}
