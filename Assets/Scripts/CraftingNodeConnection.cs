using System.Collections.Generic;
using UnityEngine;
public enum ConnectionPoints
{
    Upper,
    UpperRight,
    UpperLeft,
    Right,
    LowerRight,
    LowerLeft,
    Left,
    Lower,
}
public class CraftingNodeConnection : MonoBehaviour
{
    public ConnectionPoints ConnectionPoint;
    public bool Occupied;
    public float GetRotationFromPoint()
    {
        switch (ConnectionPoint)
        {
            case ConnectionPoints.Upper:
                return 0;
            case ConnectionPoints.UpperRight:
                return -45;
            case ConnectionPoints.UpperLeft:
                return 45;
            case ConnectionPoints.Right:
                return -90;
            case ConnectionPoints.LowerRight:
                return -135;
            case ConnectionPoints.LowerLeft:
                return 135;
            case ConnectionPoints.Left:
                return 90;
            case ConnectionPoints.Lower:
                return 180;
            default:
                return 0;
        }
    }


    public ConnectionPoints GetInvertedConnectionPointFromPoint(ConnectionPoints point)
    {
        switch (point)
        {
            case ConnectionPoints.Upper:
                return ConnectionPoints.Lower;
            case ConnectionPoints.UpperRight:
                return ConnectionPoints.LowerLeft;
            case ConnectionPoints.UpperLeft:
                return ConnectionPoints.LowerRight;
            case ConnectionPoints.Right:
                return ConnectionPoints.Left;
            case ConnectionPoints.LowerRight:
                return ConnectionPoints.UpperLeft;
            case ConnectionPoints.LowerLeft:
                return ConnectionPoints.UpperRight;
            case ConnectionPoints.Left:
                return ConnectionPoints.Right;
            case ConnectionPoints.Lower:
                return ConnectionPoints.Upper;
            default:
                return ConnectionPoints.Left;
        }
    }

    public List<ConnectionPoints> GetAdjacentConnectionPoints(ConnectionPoints point)
    {
        List<ConnectionPoints> adjacentPoints = new List<ConnectionPoints>();
        switch (point)
        {
            case ConnectionPoints.Upper:
                adjacentPoints.Add(ConnectionPoints.UpperLeft);
                adjacentPoints.Add(ConnectionPoints.UpperRight);
                break;
            case ConnectionPoints.UpperRight:
                adjacentPoints.Add(ConnectionPoints.Upper);
                adjacentPoints.Add(ConnectionPoints.Right);
                break;
            case ConnectionPoints.UpperLeft:
                adjacentPoints.Add(ConnectionPoints.Upper);
                adjacentPoints.Add(ConnectionPoints.Left);
                break;
            case ConnectionPoints.Right:
                adjacentPoints.Add(ConnectionPoints.UpperRight);
                adjacentPoints.Add(ConnectionPoints.LowerRight);
                break;
            case ConnectionPoints.LowerRight:
                adjacentPoints.Add(ConnectionPoints.Lower);
                adjacentPoints.Add(ConnectionPoints.Right);
                break;
            case ConnectionPoints.LowerLeft:
                adjacentPoints.Add(ConnectionPoints.Lower);
                adjacentPoints.Add(ConnectionPoints.Left);
                break;
            case ConnectionPoints.Left:
                adjacentPoints.Add(ConnectionPoints.LowerLeft);
                adjacentPoints.Add(ConnectionPoints.UpperLeft);
                break;
            case ConnectionPoints.Lower:
                adjacentPoints.Add(ConnectionPoints.LowerLeft);
                adjacentPoints.Add(ConnectionPoints.LowerRight);
                break;
        }
        return adjacentPoints;
    }


}
