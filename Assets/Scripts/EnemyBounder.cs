using UnityEngine;

public class EnemyBounder : MonoBehaviour
{
    [SerializeField] private Vector2 maxPos;
    [SerializeField] private Vector2 minPos;
    [SerializeField] float rayLength;
    [SerializeField] LayerMask layer;
    [SerializeField] Transform rightGetter;
    [SerializeField] Transform leftGetter;
    Vector2 lastLeftHit;
    Vector2 lastRightHit;
    bool foundMin;
    bool foundMax;
    public Vector2 MaxPos { get => maxPos; }
    public Vector2 MinPos { get => minPos; }

    private void OnEnable()
    {
        foundMin = false;
        foundMax = false;
        GetBounderies();
    }
    private void Start()
    {
        GetBounderies();
    }

    void GetBounderies()
    {
        int counter = 0;
        while (!foundMin && counter < 2000000)
        {
            counter++;
            GetLeftEdge();
        }
        while (!foundMax && counter < 2000000)
        {
            counter++;
            GetRightEdge();
        }
    }

    void GetLeftEdge()
    {
        rightGetter.position -= new Vector3(1, 0, 0);
        RaycastHit2D leftHit = Physics2D.Raycast(leftGetter.transform.position, Vector2.down, layer);
        if (leftHit)
        {
            lastLeftHit = leftHit.point;
        }
        else
        {
            minPos = lastLeftHit;
            foundMin = true;
        }
    }

    void GetRightEdge()
    {
        rightGetter.position += new Vector3(1, 0, 0);
        RaycastHit2D rightHit = Physics2D.Raycast(rightGetter.transform.position, Vector2.down, layer);
        if (rightHit)
        {
            lastRightHit = rightHit.point;
        }
        else
        {
            maxPos = lastRightHit;
            foundMax = true;
        }
    }
}
