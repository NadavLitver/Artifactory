using System.Collections;
using System.Text;
using UnityEngine;

public class BasicSword : Weapon
{
    //public UnityEvent onEnemyHit;
    private bool CanDash;
    public bool isDashing;
    public float dashDuration;
    public float dashCooldown;
    private float counter = 0;
    public float dashForce;
    private PlayerController player;
    private const string attackPrefix = "Attack";
    private StringBuilder stringBuilder;
    [SerializeField] private LayerMask groundLayer;
    private void Start()
    {
        CanDash = true;
        player = GameManager.Instance.assets.Player.GetComponent<PlayerController>();

    }

    public override void Mobility()
    {
        if (CanDash)
            StartCoroutine(IEDash());
    }

    IEnumerator IEDash()
    {
        player.canMove = false;
        CanDash = false;
        isDashing = true;
        player.ResetVelocity();
        player.ZeroGravity();
        counter = 0;
        player.Animator.SetTrigger("Mobility");
        Vector2 dir = new Vector2(player.transform.localScale.x, 0.1f);
        Vector2 dashVelocity = dashForce * dir;
        player.GetRb.velocity = dashVelocity;
        yield return new WaitForSeconds(dashDuration);
        player.canMove = true;
        isDashing = false;
        player.ResetGravity();
        player.GetRb.velocity = player.transform.forward;
        yield return new WaitForSeconds(dashCooldown);
        CanDash = true;
    }

    protected override void Ultimate()
    {

        //m_animator.SetTrigger("Attack");
    }

    protected override void Attack()
    {
        base.Attack();

        //switch over ability string 
        //to get to the current ability 
        //AbilityCombo.CurrentAbility
        //to get the ability index use 
        //AbilityCombo.GetAbilityIndex();
    }
    protected override void AttackPerformed()
    {
        /*if (AbilityCombo.GetAbilityIndex() == 1)
        {*/
        stringBuilder = new StringBuilder();
        string animationString;
        stringBuilder.Append(attackPrefix);
        stringBuilder.Append(AbilityCombo.GetAbilityIndex().ToString());
        animationString = stringBuilder.ToString();
        Debug.Log(animationString);
        m_animator.SetTrigger(animationString);
        //return;
        //   }
        //  m_animator.SetTrigger(AbilityCombo.CurrentAbilityData.AbilityAnimationName);
    }
    //private void OnDisable()
    //{
    //    isDashing = false;
    //}

}