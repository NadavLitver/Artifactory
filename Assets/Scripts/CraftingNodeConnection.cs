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


}
