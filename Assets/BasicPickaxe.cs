using System.Collections;
using System.Text;
using UnityEngine;

public class BasicPickaxe : Weapon
{
    [SerializeField] float radiusForWallCheck;
    PlayerController player;
    [SerializeField] float distaceFromPlayerToCheck;
    private bool Clawed;
    private Vector2 moveToPositionForDebug;
    private int playerLookdir => player.GetIsLookingRight ? 1 : -1;
    private Vector2 positionToCheckFrom => (Vector2)transform.position + (Vector2.right * distaceFromPlayerToCheck * playerLookdir);
    [SerializeField] Vector2 forceForWallJump;

    public LayerMask contactFilter;
    [SerializeField] private float maxClawTime;
    private Coroutine ClawRoutine;
    private StringBuilder stringBuilder;
    private const string attackPrefix = "Attack";


    private void Start()
    {
        player = GameManager.Instance.assets.Player.GetComponent<PlayerController>();
        GameManager.Instance.inputManager.onJumpDown.AddListener(WallJump);
    }

    private void WallJump()
    {
        if (Clawed)
        {

            player.canMove = true;
            Vector2 wallJumpVelocity = new Vector2((-playerLookdir * player.GetHorInput) * forceForWallJump.x, forceForWallJump.y);
            player.ResetGravity();
            player.ResetVelocity();
            player.RecieveForce(wallJumpVelocity);
            Clawed = false;

            if (!ReferenceEquals(ClawRoutine, null))
            {
                StopCoroutine(ClawRoutine);

            }
        }
      

    }

    protected override void Attack()
    {
        base.Attack();
    }
    protected override void Mobility()
    {
        Collider2D colliderHit = Physics2D.OverlapCircle(positionToCheckFrom, radiusForWallCheck, contactFilter);
        if (!ReferenceEquals(colliderHit, null) && colliderHit.CompareTag("Wall") && !Clawed)
        {
            if (!ReferenceEquals(ClawRoutine, null))
            {
                StopCoroutine(ClawRoutine);
            }
            ClawRoutine = StartCoroutine(IEMoveToWall(colliderHit.ClosestPoint(player.transform.position)));
        }

    }
    IEnumerator IEMoveToWall(Vector3 positionToMoveTo)
    {
        Clawed = true;
        moveToPositionForDebug = positionToMoveTo;
        player.canMove = false;
        player.ResetVelocity();
        player.ZeroGravity();
        float Counter = 0;
        Vector2 startingPosition = player.transform.position;

        while (!GameManager.Instance.generalFunctions.IsInRange(player.transform.position, positionToMoveTo, 0.1f))
        {
            player.transform.position = Vector3.Lerp(startingPosition, positionToMoveTo, Counter / 1);
            Counter += Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("PickaxeReached");
        yield return new WaitForSeconds(maxClawTime);
        Clawed = false;
        player.canMove = true;
        player.ResetGravity();

    }
    protected override void Ultimate()
    {
        base.Ultimate();
    }
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        Gizmos.DrawWireSphere(positionToCheckFrom, radiusForWallCheck);
        Gizmos.DrawWireSphere(moveToPositionForDebug, radiusForWallCheck);

    }
    protected override void AttackPerformed()
    {
       
        stringBuilder = new StringBuilder();
        string animationString;
        stringBuilder.Append(attackPrefix);
        stringBuilder.Append(AbilityCombo.GetAbilityIndex().ToString());
        animationString = stringBuilder.ToString();
        Debug.Log(animationString);
        m_animator.SetTrigger(animationString);
        
    }
}
