using UnityEngine;
using UnityEngine.Events;

public class CatchHandler : MonoBehaviour
{
    public UnityEvent<Actor> OnCaught;

    [SerializeField, Range(0, 100)] private float catchCahcne;

    public float CatchCahcne { get => catchCahcne; }

    public bool TryCatchingMonster()
    {
        if (Random.Range(0,100) > catchCahcne)
        {
            return false;
        }
        return true;
    }

    

}
