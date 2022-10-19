using System.Collections.Generic;
using UnityEngine;

public class EnemyRayData : MonoBehaviour
{
    [SerializeField] Transform fatherTransform;
    [SerializeField] float rayHeights;
    [SerializeField] float distanceBetweenRays;
    [SerializeField] float RayLength;
    [SerializeField] int amountOfRays;
    [SerializeField] List<Vector2> rayPoints;
    [SerializeField] List<Vector2> HitPoints;
    [SerializeField] LayerMask GroundMask;
    [SerializeField] bool useBoundingBox;
    [SerializeField] boundingBox m_boundingBox;
    private Vector2 startingPos;
    public List<Vector2> GetHitPoints { get => HitPoints; set => HitPoints = value; }
    private void Start()
    {
        startingPos = fatherTransform.position;
       
            m_boundingBox.maxX = (startingPos + m_boundingBox.offset).x + m_boundingBox.size * 0.5f;
            m_boundingBox.maxY = (startingPos + m_boundingBox.offset).y + m_boundingBox.size * 0.5f;
            m_boundingBox.minX = (startingPos + m_boundingBox.offset).x - m_boundingBox.size * 0.5f;
            m_boundingBox.minY = (startingPos + m_boundingBox.offset).y - m_boundingBox.size * 0.5f;

        
    }
    private void clearRayListData()
    {
        rayPoints.Clear();
        HitPoints.Clear();

    }

    public void Update()
    {
        clearRayListData();
        SetRayPoints();
        ShootRays();
    }
    void SetRayPoints()
    {
        float distanceBetween = distanceBetweenRays;
        Vector2 m_position = fatherTransform.position;
        float yPos = m_position.y + rayHeights;
        for (int i = 0; i < amountOfRays; i++)
        {
            int RayPointDirection = GetRayDirection(i);
            if (RayPointDirection == 1 && i != 0)
            {
                distanceBetween += distanceBetweenRays;
            }
            Vector2 currentRayPos = new Vector2(m_position.x + distanceBetween * RayPointDirection, yPos);
            if (useBoundingBox)
            {
                if (isPointInBox(currentRayPos))
                {
                    rayPoints.Add(currentRayPos);
                }
            }
            else
            {
                rayPoints.Add(currentRayPos);

            }
            

        }

    }
    public bool isPointInBox(Vector2 point)
    {
        
        if(point.x < m_boundingBox.maxX && point.x > m_boundingBox.minX && point.y < m_boundingBox.maxY && point.y > m_boundingBox.minY)
        {
            if (!Physics2D.OverlapPoint(point))
            {
                return true;
            }
        }
        return false;
    }
    void ShootRays()
    {
        for (int i = 0; i < rayPoints.Count; i++)
        {
            RaycastHit2D hit2D = Physics2D.Raycast(rayPoints[i], Vector2.down, RayLength, GroundMask);
            if(hit2D.collider != null)
            {
              HitPoints.Add(hit2D.collider.bounds.ClosestPoint(hit2D.point));
            }
            
            
        }
    }
    public bool isRayHitDataEmpty()
    {
        return !(HitPoints.Count >= 1);
    }
    public Vector2 GetRandomPos()
    {
        if (HitPoints.Count == 0)
            return Vector2.zero;

        int randomPoint = Random.Range(0, HitPoints.Count);
        return HitPoints[randomPoint];
    }
    int GetRayDirection(int i)=> (i % 2 == 0) ? 1 : -1;
    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            for (int i = 0; i < rayPoints.Count; i++)
            {
                Gizmos.DrawWireSphere(rayPoints[i], 0.5f);
                Gizmos.DrawRay(rayPoints[i], Vector2.down * RayLength);
            }
            for (int i = 0; i < HitPoints.Count; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(HitPoints[i], 0.5f);

            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(startingPos + m_boundingBox.offset, Vector2.one * m_boundingBox.size);
        //Gizmos.DrawWireSphere(new Vector2(m_boundingBox.maxX, m_boundingBox.maxY),1);
        //Gizmos.DrawWireSphere(new Vector2(m_boundingBox.minX, m_boundingBox.minY), 1);

    }
}
[System.Serializable]
struct boundingBox{
  public float size;
  public  Vector2 offset;

  public float maxY, minY, maxX, minX;
}
