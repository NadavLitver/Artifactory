using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickConfiner : MonoBehaviour
{
    [SerializeField] RectTransform JoystickToMove;
    private RectTransform m_Rect;
    private Vector2 startingPos;

    private void Start()
    {
        m_Rect = GetComponent<RectTransform>();
        GameManager.Instance.inputManager.OnTouchDown.AddListener(MoveJoyStick);
        GameManager.Instance.inputManager.OnTouchUp.AddListener(ReturnToStartPos);
        startingPos = JoystickToMove.position;
    }

    private void ReturnToStartPos()
    {
        JoystickToMove.position = startingPos;
    }

    private void MoveJoyStick()
    {

        Vector2 currentPosOnScreen = GameManager.Instance.inputManager.Touch_ScreenPos();
     
        if (RectTransformUtility.RectangleContainsScreenPoint(m_Rect,currentPosOnScreen))
        {
            JoystickToMove.position = currentPosOnScreen;
        }
      

    }
}
