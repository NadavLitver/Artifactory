using UnityEngine;

public class EnemyTestIdle : State
{
    GroundCheck m_groundCheck;
    WallCheck wallCheck;

    public Animator m_animator;
    public Transform fatherTransform;
    public float moveSpeed;
    public float SightRange;
    private Transform playerTransform => GameManager.Instance.assets.playerActor.transform;
    private bool isPlayerInRange => GameManager.Instance.generalFunctions.IsInRange(transform.position, playerTransform.position, SightRange);
    private Vector3 startScale;

    public EnemyTestStateChase chaseState;
    private void Start()
    {
        m_groundCheck = GetComponentInParent<GroundCheck>();
        wallCheck = GetComponentInParent<WallCheck>();
        startScale = fatherTransform.localScale;
    }
    public override State RunCurrentState()
    {

        m_animator.SetBool("Grounded", true);
        m_animator.SetBool("Running", true);
        bool isSwitch = (!m_groundCheck.isGrounded() || wallCheck.IsInfrontWall());
        if (isSwitch)
        {
            fatherTransform.localScale = new Vector3(fatherTransform.localScale.x * -1, startScale.y, startScale.z);
        }
     
        fatherTransform.position += Vector3.right * fatherTransform.localScale.x * moveSpeed * Time.deltaTime;

        if (isPlayerInRange)
        {
            return chaseState;
        }
        return this;
    }
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            return;
        Gizmos.color = isPlayerInRange ? Color.red : Color.green;
        Gizmos.DrawWireSphere(fatherTransform.position, SightRange);
    }

}
