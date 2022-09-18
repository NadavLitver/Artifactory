using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [Header("Burn Properties"), Space(10)]
    public float burnDuration;
    public float burnDamage;
    [Tooltip("How often do you want the burn to trigger? ")] public float BurnIntervals;
    
}
