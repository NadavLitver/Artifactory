using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineHandler : CinemachineExtension
{
    CinemachineVirtualCamera virtualCamera;
    CinemachineFramingTransposer framingTransposer;
    private float startingOffsetX;
    [SerializeField] float horizontalSpeed;
    private bool isPlayerLookingRight => GameManager.Instance.assets.PlayerController.GetIsLookingRight; 

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(vcam.Follow)
        {
            if (isPlayerLookingRight)
            {
                framingTransposer.m_TrackedObjectOffset.x = Mathf.MoveTowards(framingTransposer.m_TrackedObjectOffset.x, startingOffsetX, deltaTime * horizontalSpeed);
            }
            else
            {
                framingTransposer.m_TrackedObjectOffset.x = Mathf.MoveTowards(framingTransposer.m_TrackedObjectOffset.x, -startingOffsetX, deltaTime * horizontalSpeed);
            }
        }
    }

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        startingOffsetX = framingTransposer.m_TrackedObjectOffset.x;
    }
    
}
