using UnityEngine;
using UnityEngine.Events;
public class Weapon : MonoBehaviour
{
    protected Animator m_animator;
    public UnityEvent onHit;
    public UnityEvent<Actor, Ability> OnActorHit;
    public UnityEvent OnGroundAttack;
    [SerializeField] AbilityCombo abilityCombo;
    public RuntimeAnimatorController runTimeAnimator;
    public AbilityCombo AbilityCombo { get => abilityCombo; set => abilityCombo = value; }

    [SerializeField] private Actor host;
    public Actor Host { get => host; set => host = value; }

    private void Awake()
    {
        m_animator = GetComponentInParent<Animator>();
        host = GetComponentInParent<Actor>();
        if (!ReferenceEquals(abilityCombo, null))
        {
            abilityCombo.OnAttackPerformed.AddListener(AttackPerformed);
        }
    }
    protected virtual void Attack()
    {
        abilityCombo.PlayNextAbility();
    }
    protected virtual void AttackPerformed()
    {

    }


    public virtual void Mobility()
    {

    }
    protected virtual void Ultimate()
    {

    }
    protected virtual void OnWeaponHit(Collider2D collision)
    {
        Actor currentActorHit = collision.GetComponent<Actor>();
        if (!ReferenceEquals(currentActorHit, null))
        {
            currentActorHit.GetHit(abilityCombo.CurrentAbility, Host);
            OnActorHit?.Invoke(currentActorHit, AbilityCombo.CurrentAbility);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnWeaponHit(collision);
        onHit?.Invoke();
    }

    public virtual void Initialize()
    {
        Subscribe();
    }
    public virtual void Subscribe()
    {
        GameManager.Instance.inputManager.onAttackDown.AddListener(Attack);
        GameManager.Instance.inputManager.onMobilityDown.AddListener(Mobility);
        GameManager.Instance.inputManager.onUltimateDown.AddListener(Ultimate);
    }
    public virtual void UnSubscribe()
    {
        GameManager.Instance.inputManager.onAttackDown.RemoveListener(Attack);
        GameManager.Instance.inputManager.onMobilityDown.RemoveListener(Mobility);
        GameManager.Instance.inputManager.onUltimateDown.RemoveListener(Ultimate);
    }

}
