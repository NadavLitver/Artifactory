using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //viewArea is your player and change the off position to make the player in the center of the screen as per you want. Try changing its y axis up or down, and same with the x and z axis.
    [SerializeField] private Transform viewTarget;
    [SerializeField] Vector3 off;
    // This is how smoothly your camera follows the player
    [SerializeField]
    [Range(0, 3)]
    private float smoothness = 0.175f;
    private Vector3 velocity = Vector3.zero;
    Vector3 desiredPosition;

    private void Update()
    {
         desiredPosition = viewTarget.position + off;
    }
    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothness);

    }
}
