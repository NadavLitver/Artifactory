using UnityEngine;

[System.Serializable]
public class CustomPos
{
    [SerializeField] int x;
    [SerializeField] int y;

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }


    public bool EquatePos(CustomPos givenPos)
    {
        if (givenPos.X == x && givenPos.Y == y)
        {
            return true;
        }
        else return false;
    }


    public Vector3 vectorMult(Vector3 givenV)
    {
        return new Vector3(givenV.x * x, givenV.y * y, 0);
    }

    public override string ToString()
    {
        return $"({X} , {Y})";
    }
}
