using UnityEngine;
[ExecuteInEditMode]
public class RadixSensors : MonoBehaviour
{
    [SerializeField] Actor RadixActor;
    public float rangedRange = 10f; // Range for ranged attack
    public float meleeRange = 2f; // Range for melee attack
    public Transform player; // Reference to player's transform

    public bool isPlayerInRangeRanged = false; // True if player is in ranged range
    public bool isPlayerInRangeMelee = false; // True if player is in melee range
    public bool isRadixUnderHalfHP => RadixActor.currentHP / RadixActor.maxHP < 0.5;
    float DistanceToPlayer => Vector3.Distance(transform.position, player.position);
    private void Start()
    {
        player = GameManager.Instance.assets.Player.transform;
    }

    void Update()
    {
        // Calculate distance between player and boss


        // Check if player is in ranged range
        if (DistanceToPlayer <= rangedRange)
        {
            isPlayerInRangeRanged = true;
            isPlayerInRangeMelee = false;
        }
        else
        {
            isPlayerInRangeRanged = false;
        }

        // Check if player is in melee range
        if (DistanceToPlayer <= meleeRange)
        {
            isPlayerInRangeMelee = true;
            isPlayerInRangeRanged = false;
        }
        else
        {
            isPlayerInRangeMelee = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw gizmos to visualize the ranges
        Gizmos.color = isPlayerInRangeRanged ? Color.green : Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangedRange);

        Gizmos.color = isPlayerInRangeMelee ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}
