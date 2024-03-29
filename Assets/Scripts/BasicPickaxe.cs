using System.Collections;
using System.Text;
using UnityEngine;

public class BasicPickaxe : Weapon
{
    [SerializeField] float radiusForWallCheck;
    PlayerController player => GameManager.Instance.assets.Player.GetComponent<PlayerController>();
    [SerializeField] float distaceFromPlayerToCheck;
    [SerializeField] Vector2 JumpToClawForce;
    private bool Clawed;
    private Vector2 moveToPositionForDebug;
    private int playerLookdir => player.GetIsLookingRight ? 1 : -1;
    private Vector2 jumpToClawForce => new Vector2(JumpToClawForce.x * playerLookdir, JumpToClawForce.y);
    private Vector2 positionToCheckFrom => (Vector2)transform.position + (Vector2.right * distaceFromPlayerToCheck * playerLookdir);
    [SerializeField] Vector2 forceForWallJump;

    public LayerMask contactFilter;
    [SerializeField] private float maxClawTime;
    private Coroutine ClawRoutine;
    private StringBuilder stringBuilder;
    [SerializeField] private float downForceFromAirAttack;
    private const string attackPrefix = "Attack";

    [SerializeField] private float groundedAttackTime;
    [SerializeField] private float mobilityCD;
    bool airAttacking;
    private bool canMobility;
    GameObject clawEffect;
    [SerializeField] ObjectPool clawEffectPool;
    Vector3 originaClawEffectlRot;

    private int ClimbHash;
    private int MobilityHash;
    private void Start()
    {
        //  player = GameManager.Instance.assets.Player.GetComponent<PlayerController>();
        GameManager.Instance.inputManager.onJumpDown.AddListener(CallWallJump);
        airAttacking = false;
        originaClawEffectlRot = clawEffectPool.GetPooledObject().transform.eulerAngles;
        ClimbHash = Animator.StringToHash("Climb");
        MobilityHash = Animator.StringToHash("Mobility");
        canMobility = true;
    }
    public void CallWallJump()
    {
        canMobility = true;
        WallJump();
    }
    private void WallJump()
    {   
        if (Clawed)
        {
            player.Animator.SetBool(ClimbHash, false);
            player.canMove = true;
            Vector2 wallJumpVelocity = forceForWallJump;
            
            if (playerLookdir == -1 && (player.GetHorInput == -1 || player.GetHorInput == 0))
            {
                wallJumpVelocity = new Vector2(0, forceForWallJump.y);
            }
            else if (playerLookdir == 1 && player.GetHorInput == -1)
            {
                wallJumpVelocity = new Vector2(-forceForWallJump.x, forceForWallJump.y);
            }
            else if (playerLookdir == 1 && (player.GetHorInput == 1 || player.GetHorInput == 0))
            {
                wallJumpVelocity = new Vector2(0, forceForWallJump.y);
            }
            if (gameObject.activeInHierarchy)
            {
                player.ResetGravity();
                player.ResetVelocity();
                player.RecieveForce(wallJumpVelocity);
            }
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
    public override void Mobility()
    {
        if (!canMobility)
            return;

        if (Clawed)
        {

            m_animator.SetTrigger(MobilityHash);
            StartCoroutine(IEJumpFromClawedWithMobility());
            return;
        }
        if (player.GetIsGrounded)
        {
            m_animator.SetTrigger(MobilityHash);
            StartCoroutine(IEJumpFromMobility());
        }
        else
        {
            m_animator.SetTrigger(MobilityHash);
            StartCoroutine(MobilityInAir());
        }
        StartCoroutine(IEMobilityCD());

    }

    private void CheckFromWall()
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
    IEnumerator MobilityInAir()
    {
        while (!player.GetIsGrounded && !Clawed)
        {
            CheckFromWall();
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator IEJumpFromMobility()
    {
        player.ResetVelocity();
        StartCoroutine(player.TogglePlayingTraversal());
        player.RecieveForce(jumpToClawForce);

        yield return new WaitUntil(() => player.GetIsFalling == true);
        yield return MobilityInAir();

        //while (!player.GetIsGrounded && !Clawed)
        //{
        //    CheckFromWall();
        //    yield return new WaitForEndOfFrame();
        //}
    }
    IEnumerator IEMobilityCD()
    {
        if (!canMobility)
        yield break;

        canMobility = false;
        yield return new WaitForSeconds(mobilityCD);
        canMobility = true;
    }
    IEnumerator IEJumpFromClawedWithMobility()
    {
        //player.ResetVelocity();
        //player.RecieveForce(jumpToClawForce);
        WallJump();
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => player.GetIsFalling == true);
        yield return MobilityInAir();
    }
    IEnumerator IEMoveToWall(Vector3 positionToMoveTo)
    {
        m_animator.SetTrigger("Climb");
        Clawed = true;
        moveToPositionForDebug = positionToMoveTo;
        player.canMove = false;
        airAttacking = false;
        player.Animator.SetBool("Climb", true);
        player.ResetVelocity();
        player.ZeroGravity();
        float Counter = 0;
        Vector2 startingPosition = player.transform.position;

        while (!GameManager.Instance.generalFunctions.IsInRange(player.transform.position, positionToMoveTo, 0.3f))
        {
            player.transform.position = Vector3.Lerp(startingPosition, positionToMoveTo, Counter / 1);
            Counter += Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("PickaxeReached");
        TurnOnClawEffect();
        Counter = 0;
        while (Counter < maxClawTime)
        {
         
            player.Animator.SetBool("IsLookingAwayFromWall", -player.GetHorInput == playerLookdir);
            Counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
        // yield return new WaitForSeconds(maxClawTime);
        player.Animator.SetBool("Climb", false);
        Clawed = false;
        player.canMove = true;
        player.ResetGravity();

    }

    private void TurnOnClawEffect()
    {
        clawEffect = clawEffectPool.GetPooledObject();
        clawEffect.transform.eulerAngles = originaClawEffectlRot;
        if (!GameManager.Instance.assets.PlayerController.GetIsLookingRight)
        {
            clawEffect.transform.eulerAngles = new Vector3(originaClawEffectlRot.x, originaClawEffectlRot.y, originaClawEffectlRot.z * -1); ;
        }
        clawEffect.transform.position = GameManager.Instance.assets.PlayerController.ClawEffectPoint.position;
        clawEffect.SetActive(true);
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
        if (!player.GetIsGrounded)
        {
            if (airAttacking)
                return;

            m_animator.SetTrigger("Attack1");
            StartCoroutine(AirAttack());
            return;
        }

        stringBuilder = new StringBuilder();
        string animationString;
        stringBuilder.Append(attackPrefix);
        stringBuilder.Append(AbilityCombo.GetAbilityIndex().ToString());
        animationString = stringBuilder.ToString();
        Debug.Log(animationString);
        m_animator.SetTrigger(animationString);
        OnGroundAttack?.Invoke();
    }

    private IEnumerator AirAttack()
    {
        player.ResetVelocity();
        airAttacking = true;
        player.canMove = false;
        yield return new WaitForSeconds(0.1f);
        while (!player.GetIsGrounded)
        {
            player.GetRb.velocity = Vector2.down * downForceFromAirAttack;
            yield return new WaitForEndOfFrame();
        }
        player.canMove = true;
        airAttacking = false;
    }
    private void OnDisable()
    {
        player.canMove = true;
        airAttacking = false;
        Clawed = false;
        player.ResetGravity();
        OnGroundAttack.RemoveListener(StopPlayerWhileGroundAttacking);
    }

    private void OnEnable()
    {
        OnGroundAttack.AddListener(StopPlayerWhileGroundAttacking);
    }

    private void StopPlayerWhileGroundAttacking()
    {
        StartCoroutine(StopPlayer());
    }

    private IEnumerator StopPlayer()
    {
        player.ResetVelocity();
        player.canMove = false;
        yield return new WaitForSeconds(groundedAttackTime);
        player.canMove = true;
    }
}
