using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class JoystickConfiner : MonoBehaviour
{
    [SerializeField] RectTransform JoystickToMove;
    private RectTransform m_Rect;
    private Vector2 startingPos;
    bool wasPrimary;
    private void Start()
    {
        m_Rect = GetComponent<RectTransform>();

        GameManager.Instance.inputManager.OnPrimaryTouchDown.AddListener(MoveJoyStick);
        GameManager.Instance.inputManager.OnSecondaryTouchDown.AddListener(MoveJoyStick);
        GameManager.Instance.inputManager.OnPrimaryTouchUp.AddListener(ReturnToStartPos);
        GameManager.Instance.inputManager.OnSecondaryTouchUp.AddListener(ReturnToStartPos);
        startingPos = JoystickToMove.position;
    }

    private void ReturnToStartPos(bool isPrimary)
    {
        if (isPrimary == wasPrimary)
        {
            JoystickToMove.position = startingPos;
        }
    }

    private void MoveJoyStick(bool isPrimary)
    {
        if (isPrimary)
        {
            Vector2 currentPosOnScreen = GameManager.Instance.inputManager.Touch_ScreenPos();
            if (RectTransformUtility.RectangleContainsScreenPoint(m_Rect, currentPosOnScreen))
            {
                JoystickToMove.position = currentPosOnScreen;
                wasPrimary = true;
            }
        }
        else
        {
            Vector2 currentPosOnScreenSecondary = GameManager.Instance.inputManager.SecondaryTouch_ScreenPos();
            if (RectTransformUtility.RectangleContainsScreenPoint(m_Rect, currentPosOnScreenSecondary))
            {
                JoystickToMove.position = currentPosOnScreenSecondary;
                wasPrimary = false;

            }

        }
     
        // Debug.Log("CurrentPositionOnScreen Primary :" + currentPosOnScreen + "CurrentPositionOnScreen Secondary" + currentPosOnScreenSecondary);
      
      


    }
  
  

   
}
