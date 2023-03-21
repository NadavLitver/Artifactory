using System.Collections;
using System.Text;
using UnityEngine;

public class BasicSword : Weapon
{
    //public UnityEvent onEnemyHit;
    public bool isDashing;
    public float dashDuration;
    public float dashCooldown;
    private float lastDash;
    private float counter = 0;
    public float dashForce;
    private bool canDash;
    [SerializeField] float floatTimeInAirBeforeDash = 0.12f;
    [SerializeField] float GravityScaleBeforeDash;
    private PlayerController player => GameManager.Instance.assets.PlayerController;
    private const string attackPrefix = "Attack";
    private StringBuilder stringBuilder;
    [SerializeField] private LayerMask groundLayer;

    public override void Mobility()
    {
        if (Time.time - lastDash >= dashCooldown && canDash)
            StartCoroutine(IEDash());
    }

    private void Start()
    {
        GameManager.Instance.assets.PlayerController.OnsGroundCheck1.OnGrounded.AddListener(ResetCanDash);
    }
    private void ResetCanDash()
    {
        canDash = true;
    }

    IEnumerator IEDash()
    {
        yield return player.FreezePlayerForDuration(floatTimeInAirBeforeDash, GravityScaleBeforeDash);
        lastDash = Time.time;
        player.canMove = false;
        player.ResetVelocity();
        player.ZeroGravity();
        counter = 0;
        player.Animator.SetTrigger("Mobility");
        Vector2 dir = new Vector2(player.transform.localScale.x, 0.1f);
        Vector2 dashVelocity = dashForce * dir;
        player.GetRb.velocity = dashVelocity;
        StartCoroutine(DashDuration());
        player.RightSensors.gameObject.SetActive(true);
        player.LeftSensors.gameObject.SetActive(true);
        yield return new WaitUntil(() => !isDashing || CheckSensrosOutSideOfBase());
        player.RightSensors.gameObject.SetActive(false);
        player.LeftSensors.gameObject.SetActive(false);
        player.ResetGravity();
        StartCoroutine(player.JumpApexWait());
        player.canMove = true;
    }

    private bool CheckSensrosOutSideOfBase()
    {
        if (!GameManager.Instance.GameStarted)
        {
            return false;
        }
        return player.CheckForSideSensorsCollision();
    }


    private IEnumerator DashDuration()
    {
        isDashing = true;
        yield return new WaitForSecondsRealtime(dashDuration);
        isDashing = false;
    }

    protected override void Ultimate()
    {

        //m_animator.SetTrigger("Attack");
    }

    protected override void Attack()
    {
        base.Attack();
    }
    protected override void AttackPerformed()
    {
        stringBuilder = new StringBuilder();
        string animationString;
        stringBuilder.Append(attackPrefix);
        stringBuilder.Append(AbilityCombo.GetAbilityIndex().ToString());
        animationString = stringBuilder.ToString();
        // Debug.Log(animationString);
        m_animator.SetTrigger(animationString);
    }
    private void OnDisable()
    {
        canDash = false;
        player.canMove = true;
        isDashing = false;
        player.ResetGravity();
    }

    private void OnEnable()
    {
        if (GameManager.Instance.assets.PlayerController.GetIsGrounded)
        {
            canDash = true;
        }
    }
}