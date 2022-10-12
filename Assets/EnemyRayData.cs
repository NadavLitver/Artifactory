using System.Collections.Generic;
using UnityEngine;

public class EnemyRayData : MonoBehaviour
{
    [SerializeField] Transform fatherTransform;
    [SerializeField] float maxJumpHeight;
    [SerializeField] float distanceBetweenRays;
    [SerializeField] float RayLength;
    [SerializeField] int amountOfRays;
    [SerializeField] List<Vector2> rayPoints;
    [SerializeField] List<Vector2> HitPoints;
    [SerializeField] LayerMask GroundMask;
    private void InitRayPoints()
    {
        if (rayPoints.Count < amountOfRays)
        {
            int missingRays = amountOfRays - rayPoints.Count;
            for (int i = 0; i < missingRays; i++)
            {
                rayPoints.Add(Vector2.zero);
                HitPoints.Add(Vector2.zero);
            }
        }
        else if (rayPoints.Count > amountOfRays)
        {
            int missingRays = rayPoints.Count - amountOfRays;
            for (int i = 0; i < missingRays; i++)
            {
                rayPoints.RemoveAt(rayPoints.Count -1);
                HitPoints.RemoveAt(HitPoints.Count - 1);
                
            }
        }

    }

    public void Update()
    {
        InitRayPoints();
        SetRayPoints();
        ShootRays();
    }
    void SetRayPoints()
    {
        float distanceBetween = distanceBetweenRays;
        Vector2 m_position = fatherTransform.position;
        float yPos = m_position.y + maxJumpHeight;
        for (int i = 0; i < amountOfRays; i++)
        {
            int RayPointDirection = GetRayDirection(i);
            if (RayPointDirection == 1 && i != 0)
            {
                distanceBetween += distanceBetweenRays;
            }
            rayPoints[i] = new Vector2(m_position.x + distanceBetween * RayPointDirection, yPos);

        }

    }
    void ShootRays()
    {
        for (int i = 0; i < amountOfRays; i++)
        {
            RaycastHit2D hit2D = Physics2D.Raycast(rayPoints[i], Vector2.down, RayLength, GroundMask);
            HitPoints[i] = hit2D.point;
            
            
        }
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
    
}
