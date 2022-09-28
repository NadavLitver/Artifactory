using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BasicSword : Weapon
{
    //public UnityEvent onEnemyHit;
    private int CurrentAttack;
    private float LastAttackTime;
    public Ability ComboFinalAttack;
    public float attack1Recovery, attack2Recovery, attack3Recovery;
    public bool CanAttack;
    private bool CanDash;
    public bool isDashing;
    public float dashDuration;
    public float dashCooldown;
    private float counter = 0;
    public float dashForce;
    private PlayerController player;

    private void Start()
    {
        CurrentAttack = 1;
        LastAttackTime = 0;
        CanAttack = true;
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
        float dir = player.transform.localScale.x;
        Vector2 dashVelocity = new Vector2(dashForce * dir, 0);
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
    protected override void Attack()
    {
        if (!CanAttack)
            return;
        currentAbility = m_ability;
        switch (CurrentAttack)
        {
            case 1:
                ExecuteFirstAttack();
                break;
            case 2:
                if (Time.time - LastAttackTime < attack2Recovery)
                {
                    ExecuteSecondAttack();
                }
                else
                {
                    ResetComboAndExecuteAttack();
                }

                break;
            case 3:
                if (Time.time - LastAttackTime < attack3Recovery && player.GetHorInput == 0)
                {
                    ExecuteThirdAttack();
                }
                else
                {
                    ResetComboAndExecuteAttack();

                }
                break;
            default:
                break;
        }
    }

    private void ExecuteFirstAttack()
    {
        m_animator.Play("Attack");
        LastAttackTime = Time.time;
        CurrentAttack = 2;
        CanAttack = false;
        LeanTween.delayedCall(0.5f, SetCanAttackTrue);
        Debug.Log(CurrentAttack);
    }
    private void ExecuteSecondAttack()
    {
        m_animator.SetTrigger("Attack2");
        LastAttackTime = Time.time;
        CurrentAttack = 3;
        CanAttack = false;
        LeanTween.delayedCall(0.5f, SetCanAttackTrue);
        Debug.Log(CurrentAttack);

    }
    private void ExecuteThirdAttack()
    {
        currentAbility = ComboFinalAttack;
        m_animator.SetTrigger("Attack3");
        LastAttackTime = Time.time;
        CanAttack = false;
        LeanTween.delayedCall(2f, ResetCombo);


    }
    private void ResetComboAndExecuteAttack()
    {
        CurrentAttack = 1;
        Debug.Log(CurrentAttack);
        ExecuteFirstAttack();
    }
    private void ResetCombo()
    {
        CurrentAttack = 1;
        SetCanAttackTrue();
        Debug.Log(CurrentAttack);
    }
    private void SetCanAttackTrue() => CanAttack = true;
    protected override void Ultimate()
    {

        currentAbility = m_UltimateAbility;
        m_animator.Play("Attack");

    }

    protected override void OnWeaponHit(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Actor currentEnemyHit = collision.GetComponent<Actor>();
            if (ReferenceEquals(currentEnemyHit, null))
            {
                currentEnemyHit = collision.GetComponentInChildren<Actor>();
                if (ReferenceEquals(currentEnemyHit, null))
                {
                    currentEnemyHit = collision.GetComponentInParent<Actor>();

                }
            }
            if (!ReferenceEquals(currentEnemyHit, null))
            {
                // currentEnemyHit.TakeDamage(damage);
                currentEnemyHit.GetHit(currentAbility);
               // onEnemyHit.Invoke();
            }
        }

    }
   
    
}