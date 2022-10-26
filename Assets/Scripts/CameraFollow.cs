using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //viewArea is your player and change the off position to make the player in the center of the screen as per you want. Try changing its y axis up or down, and same with the x and z axis.
    [SerializeField] private Transform viewTarget;
    [SerializeField] Vector3 off;
    // This is how smoothly your camera follows the player
    //[SerializeField, Range(0, 3)]
    //private float smoothness = 0.175f;
    //private Vector3 velocity = Vector3.zero;
    Vector3 desiredPosition;
    private float xOffset;
    private PlayerController playerController;
    [SerializeField, Range(0, 100)] float followSpeed;
    private bool isPlayerLookingRight => playerController.GetIsLookingRight;
    public bool locked;
    
    [SerializeField] float HorizontalOffsetChangeSpeed;

    private void Start()
    {
        xOffset = off.x;
        playerController = GameManager.Instance.assets.PlayerController;
        locked = false;
    }

    private void Update()
    {
        if (!locked)
        {
            desiredPosition = viewTarget.position + off;

        }
        if (isPlayerLookingRight)
        {
            off.x = Mathf.MoveTowards(off.x, xOffset, Time.deltaTime * HorizontalOffsetChangeSpeed);
        }
        else
        {
            off.x = Mathf.MoveTowards(off.x, -xOffset, Time.deltaTime * HorizontalOffsetChangeSpeed);
        }
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * followSpeed);
    }
    //private void LateUpdate()
    //{
     
    //}
}
