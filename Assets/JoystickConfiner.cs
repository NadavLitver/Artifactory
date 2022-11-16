using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickConfiner : MonoBehaviour
{
    [SerializeField] RectTransform JoystickToMove;
    private Camera m_camera;
    private RectTransform m_Rect;
    private Vector2 startingPos;

    private void Start()
    {
        m_Rect = GetComponent<RectTransform>();
        m_camera = Camera.main;
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
        Debug.Log("CurrentTouchPos = " + currentPosOnScreen + "rectOffset = " + m_Rect.offsetMax);
        //if(RectTransformUtility.ScreenPointToLocalPointInRectangle(m_Rect,currentPosOnScreen,Camera.main,out Vector2 localPoint))
        //{
        //    JoystickToMove.position = currentPosOnScreen;

        //}
        if (RectTransformUtility.RectangleContainsScreenPoint(m_Rect,currentPosOnScreen))
        {
            JoystickToMove.position = currentPosOnScreen;
        }
        //if (currentPosOnScreen.x < m_Rect.rect.max.x && currentPosOnScreen.y < m_Rect.rect.max.y && currentPosOnScreen.x > -m_Rect.rect.max.x && currentPosOnScreen.y > -m_Rect.rect.max.y)
        //    JoystickToMove.position = currentPosOnScreen;


    }
}
