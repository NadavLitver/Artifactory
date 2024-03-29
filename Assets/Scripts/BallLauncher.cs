using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class BallLauncher : MonoBehaviour
{

    public Rigidbody2D ball;
    public EnemyRayData rayData;
    public Vector2 RandomTarget => rayData.GetRandomPos();
    public float h = 25;
    public float gravity = -18;

    public bool debugPath;
    private float startingGravity;
    private float debugCounter;
    public float debugIntrevals;

    public bool IsJumping;

    public UnityEvent LandedEvent;


    void Update()
    {
        if (debugPath)
        {
            if (debugCounter > debugIntrevals)
            {
                debugCounter = 0;
            }
            for (int i = 0; i < rayData.GetHitPoints.Count; i++)
            {
                DrawPath(CalculateLaunchData(rayData.GetHitPoints[i]));
            }
            debugCounter += Time.deltaTime;
        }
    }
    [ContextMenu("Launch")]
    public void Launch(Vector2 target)
    {
        if (rayData.isRayHitDataEmpty())
            return;
        LaunchData currentLaunchData = CalculateLaunchData(target);
        StartCoroutine(IEStopWhenTimeOver(currentLaunchData));
       
        ball.velocity = currentLaunchData.initialVelocity;//use the calc func to get new one

    }

    LaunchData CalculateLaunchData(Vector2 target)
    {
        float displacementY = target.y - ball.position.y;
        Vector2 displacementXZ = new Vector2(target.x - ball.position.x, 0);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector2 velocityY = Vector2.up * Mathf.Sqrt(-2 * gravity * h);
        Vector2 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }
    IEnumerator IEStopWhenTimeOver(LaunchData launchData)
    {
        IsJumping = true;
        yield return new WaitForSeconds(launchData.timeToTarget);
        IsJumping = false;
        LandedEvent?.Invoke();
        //landed
        ball.velocity = Vector2.zero;

    }
    void DrawPath(LaunchData launchData)
    {
        Vector3 previousDrawPoint = ball.position;
        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector2 displacement = launchData.initialVelocity * simulationTime + Vector2.up * gravity * simulationTime * simulationTime / 2f;
            Vector2 drawPoint = ball.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector2 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector2 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
    private void OnDisable()
    {
        IsJumping = false;
    }
}