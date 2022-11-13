using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CamPositionSetter : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float delayBetweenUpdates;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask layerToCheck;
    [SerializeField] Vector3 offset;
    public bool follow;

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    CinemachineFramingTransposer framingTransposer;
    public Vector2 smallestPoint;
    public Vector2 biggestPoint;
    public Collider2D[] collidersFound;
    Vector2 finalPoint;
    public AnimationCurve DeathDollyInCurve;

    public CinemachineVirtualCamera VirtualCamera { get => virtualCamera; set => virtualCamera = value; }

    public void Start()
    {
        follow = true;
        playerTransform = GameManager.Instance.assets.Player.transform;
        GameManager.Instance.assets.playerActor.OnDeath.AddListener(OnPlayerDeathCamBehave);
       
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void OnPlayerDeathCamBehave()
    {
        virtualCamera.Follow = playerTransform;
        StartCoroutine(lerpCameraDollyIn());
    }
    IEnumerator lerpCameraDollyIn()
    {
        float counter = 0;
        float startingFOV = virtualCamera.m_Lens.FieldOfView;
        float goal = 30;
        while (counter < 1)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(startingFOV, goal, DeathDollyInCurve.Evaluate(counter));
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    void CalcPoints()
    {
        smallestPoint = playerTransform.position;
        biggestPoint = playerTransform.position;
        foreach (var item in collidersFound)
        {
            if (smallestPoint.x > item.transform.position.x)
            {
                smallestPoint.x = item.transform.position.x;
            }
            if (smallestPoint.y > item.transform.position.y)
            {
                smallestPoint.y = item.transform.position.y;
            }
            //
            if (biggestPoint.x < item.transform.position.x)
            {
                biggestPoint.x = item.transform.position.x;
            }
            if (biggestPoint.y < item.transform.position.y)
            {
                biggestPoint.y = item.transform.position.y;
            }
        }
       
        finalPoint = new Vector2((smallestPoint.x + biggestPoint.x + playerTransform.position.x) / 3, (smallestPoint.y + biggestPoint.y + playerTransform.position.y) /3);
        
    }
    public void FixedUpdate()
    {
        if (follow)
        {
            collidersFound = Physics2D.OverlapCircleAll(playerTransform.position, checkRadius, layerToCheck);
            CalcPoints();
            transform.position = Vector2.MoveTowards(transform.position, finalPoint, Time.deltaTime);
        
        }

    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(playerTransform.position, checkRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(finalPoint, 1);

    }
}
