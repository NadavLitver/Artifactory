using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckNew : MonoBehaviour
{
    [SerializeField] List<GroundCheckSensor> sensors = new List<GroundCheckSensor>();
    [SerializeField] LayerMask hitLayer;
    public bool IsAllGrounded()
    {
        foreach (var item in sensors)
        {
            Vector3 relativePos = new Vector3(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);
            if (!Physics2D.Raycast(relativePos, item.Direcion, item.Range, hitLayer))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsGrounded()
    {
        foreach (var item in sensors)
        {
            Vector3 relativePos = new Vector3(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);
            if (Physics2D.Raycast(relativePos, item.Direcion, item.Range, hitLayer))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsSensorGrounded(GroundCheckSensor givenSensor)
    {
        Vector3 relativePos = new Vector3(transform.position.x + givenSensor.Offset.x, transform.position.y + givenSensor.Offset.y);
        if (Physics2D.Raycast(relativePos, givenSensor.Direcion, givenSensor.Range, hitLayer))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var item in sensors)
        {
            Vector3 relativePos = new Vector3(transform.position.x + item.Offset.x, transform.position.y + item.Offset.y);
            Vector3 to = item.Direcion * item.Range;
            to = new Vector3(relativePos.x + to.x, relativePos.y + to.y);
            if (IsSensorGrounded(item))
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawLine(relativePos, to);
        }
    }

}
[System.Serializable]
public class GroundCheckSensor
{
    public Vector2 Direcion;
    public Vector2 Offset;
    public float Range;
}