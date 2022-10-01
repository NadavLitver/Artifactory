using System.Collections;
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

    private void Start()
    {
        CanDash = true;
        player = GameManager.Instance.assets.Player.GetComponent<PlayerController>();
    }

    protected override void Mobility()
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
        Vector2 dir = new Vector2(player.transform.localScale.x, 0.1f);
        Vector2 dashVelocity = dashForce * dir;
        while (counter < dashDuration)
        {
            counter += Time.deltaTime;
            player.GetRb.velocity = dashVelocity;
            yield return new WaitForEndOfFrame();
        }
        player.canMove = true;
        isDashing = false;
        player.ResetGravity();
        yield return new WaitForSeconds(dashCooldown);
        CanDash = true;
    }

    protected override void Ultimate()
    {

        m_animator.Play("Attack");
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
        m_animator.Play("Attack");
        //return;
        //   }
        //  m_animator.SetTrigger(AbilityCombo.CurrentAbilityData.AbilityAnimationName);
    }
}