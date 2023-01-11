using System.Collections;
using UnityEngine;
public class EnemyBounder : MonoBehaviour
{
    [SerializeField] private Vector2 maxPos;
    [SerializeField] private Vector2 minPos;
    [SerializeField] float rayLength;
    [SerializeField] LayerMask layer;
    [SerializeField] BounderScout rightGetter;
    [SerializeField] BounderScout leftGetter;
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
            StartCoroutine(rightGetter.flyTowards(Vector3.right, timeOut));
            StartCoroutine(leftGetter.flyTowards(Vector3.left, timeOut));
            StartCoroutine(WaitUntilDone());
        }
    }

    public void CachePoints(Vector2 max, Vector2 min)
    {
        maxPos = max;
        minPos = min;
    }


    private void SetLeftPoint()
    {
        foundLeft = true;
        minPos = leftGetter.transform.position;
    }

    private void SetRightPoint()
    {
        foundRight = true;
        maxPos = rightGetter.transform.position;
    }

    IEnumerator WaitUntilDone()
    {
        yield return new WaitUntil(() => rightGetter.reached && leftGetter.reached);
        SetRightPoint();
        SetLeftPoint();
        Done = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(MaxPos, 1);
        Gizmos.DrawWireSphere(MinPos, 1);
    }


}
