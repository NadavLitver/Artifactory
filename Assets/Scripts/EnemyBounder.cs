using System.Collections;
using UnityEngine;
public class EnemyBounder : MonoBehaviour
{
    [SerializeField] private Vector2 maxPos;
    [SerializeField] private Vector2 minPos;
    [SerializeField] float rayLength;
    [SerializeField] LayerMask layer;
    [SerializeField] Transform rightGetter;
    [SerializeField] Transform leftGetter;
    [SerializeField] float scoutMs;
    [SerializeField] bool getter;
    [SerializeField] float timeOut;
    float startTime;
    public bool Done;
    Vector2 lastLeftHit;
    Vector2 lastRightHit;
    bool foundLeft;
    bool foundRight;

    public Vector2 MaxPos { get => maxPos; }
    public Vector2 MinPos { get => minPos; }

    private void OnEnable()
    {
        if (!getter && !Done)
        {
            rightGetter = Instantiate(GameManager.Instance.assets.BounderScout, transform.position, Quaternion.identity);
            leftGetter = Instantiate(GameManager.Instance.assets.BounderScout, transform.position, Quaternion.identity);
            StartCoroutine(WaitUntilDone());
            StartCoroutine(GetLeftPoint());
            StartCoroutine(GetRightPoint());
        }
    }

    public void CachePoints(Vector2 max, Vector2 min)
    {
        maxPos = max;
        minPos = min;
    }

    IEnumerator GetLeftPoint()
    {
        startTime = Time.time;
        while (!foundLeft || Time.time - startTime >= timeOut)
        {
            RaycastHit2D downHit = Physics2D.Raycast(leftGetter.position, Vector2.down, rayLength, layer);
            RaycastHit2D frontHit = Physics2D.Raycast(leftGetter.position, Vector2.left, rayLength, layer);
            if (ReferenceEquals(downHit.collider, null) || !ReferenceEquals(frontHit.collider, null))
            {
                break;
            }
            else
            {
                lastLeftHit = downHit.point;
                leftGetter.Translate(Vector2.left * scoutMs * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
        SetLeftPoint();
    }

    private void SetLeftPoint()
    {
        foundLeft = true;
        minPos = new Vector2(lastLeftHit.x + 1, lastLeftHit.y);
    }

    private void SetRightPoint()
    {
        foundRight = true;
        maxPos = new Vector2(lastRightHit.x - 1, lastRightHit.y);
    }

    IEnumerator GetRightPoint()
    {
        startTime = Time.time;
        while (!foundRight || Time.time - startTime >= timeOut)
        {
            RaycastHit2D downHit = Physics2D.Raycast(rightGetter.position, Vector2.down, rayLength, layer);
            RaycastHit2D frontHit = Physics2D.Raycast(rightGetter.position, Vector2.right, rayLength, layer);
            if (ReferenceEquals(downHit.collider, null) || !ReferenceEquals(frontHit.collider, null))
            {
                break;
            }
            else
            {
                lastRightHit = downHit.point;
                rightGetter.Translate(Vector2.right * scoutMs * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
        SetRightPoint();
    }

    IEnumerator WaitUntilDone()
    {
        yield return new WaitUntil(() => foundRight && foundLeft);
        Done = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(MaxPos, 1);
        Gizmos.DrawWireSphere(MinPos, 1);
    }


}
