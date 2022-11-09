using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour, IDamagable
{
    public float currentHP;
    public float maxHP;
    public List<StatusEffect> ActorStatusEffects { get => m_StatusEffects; set => m_StatusEffects = value; }

    /// <summary>
    /// invoked when this actor takes damage
    /// </summary>
    public UnityEvent<DamageHandler> onTakeDamage { get; set; }
    /// <summary>
    /// invoked when this actor deals damage to another one
    /// </summary>
    public UnityEvent<DamageHandler, Actor> OnDealDamage;
    /// <summary>
    /// invoked when this actor is done adding damage mods(reciever)
    /// </summary>
    public UnityEvent<DamageHandler> OnDamageCalcOver;
    /// <summary>
    /// invoked when this actor is done adding damage mods(attacker)
    /// </summary>
    public UnityEvent<DamageHandler> OnDealingDamageCalcOver;
    /// <summary>
    /// invoked when this actor is hit by another actor
    /// </summary>
    public UnityEvent<Ability, Actor> OnHitByActor;
    /// <summary>
    /// invoked when this actor kills another one
    /// </summary>
    public UnityEvent<Actor> OnKill;
    /// <summary>
    /// invoked when this actor applys a status effect to another one
    /// </summary>
    public UnityEvent<StatusEffectEnum> OnApplyStatusEffect;
    /// <summary>
    /// invoked when this actor recieves a status effect
    /// </summary>
    public UnityEvent<StatusEffectEnum> OnRecieveStatusEffect;
    /// <summary>
    /// invoked when this actor loses a status effect
    /// </summary>
    public UnityEvent<StatusEffect> OnStatusEffectRemoved;
    /// <summary>
    /// invoked when this actor is healed
    /// </summary>
    public UnityEvent<DamageHandler> OnRecieveHealth;
    public UnityEvent TakeDamageGFX, OnDeath;
    public UnityEvent OnHealGFX;

    //  public UnityEvent onTakeDamage, onGetHealth, OnDeath;
    [SerializeField] private List<StatusEffect> m_StatusEffects;


    public bool IsInAttackAnim;

    [SerializeField] bool effectable;

    public void Awake()
    {
        currentHP = maxHP;
        onTakeDamage = new UnityEvent<DamageHandler>();
        m_StatusEffects = new List<StatusEffect>();
    }
    private void ClampHP() => currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    public virtual void onActorDeath()
    {
        Debug.Log(gameObject.name + "had Died");
        OnDeath?.Invoke();
    }

    public void RecieveStatusEffects(StatusEffectEnum givenStatusEffect)
    {
        StatusEffect effect = GameManager.Instance.generalFunctions.GetStatusFromType(givenStatusEffect);
        foreach (StatusEffect SE in ActorStatusEffects)
        {
            if (SE.GetType().Equals(effect.GetType()))
            {
                //duplicate type already exists
                SE.Reset();
                return;
            }
        }
        ActorStatusEffects.Add(effect);
        effect.cacheHost(this);
        OnRecieveStatusEffect?.Invoke(givenStatusEffect);
    }

    public void RemoveStatusEffect(StatusEffect givenEffect)
    {
        foreach (var item in ActorStatusEffects)
        {
            if (item.GetType().Equals(givenEffect.GetType()))
            {
                //duplicate type already exists
                ActorStatusEffects.Remove(item);
                item.Unsubscribe();
                OnStatusEffectRemoved?.Invoke(givenEffect);
                return;
            }
        }
    }

    public void GetHit(Ability givenAbility, Actor host)
    {
        if (effectable)
        {
            foreach (var SE in givenAbility.StatusEffects)
            {
                if (givenAbility.RollForStatusActivation(SE))
                {
                    RecieveStatusEffects(SE.StatusType);
                    host.OnApplyStatusEffect?.Invoke(SE.StatusType);
                }
            }
        }

        host.OnHitByActor?.Invoke(givenAbility, this);
        //invoking the hit event on the actor that hit me
        TakeDamage(givenAbility.DamageHandler, host);
    }

    public void TakeDamage(DamageHandler dmgHandler, Actor host)
    {
        host.OnDealDamage?.Invoke(dmgHandler, this);
        onTakeDamage?.Invoke(dmgHandler);
        OnDamageCalcOver?.Invoke(dmgHandler);
        host.OnDealingDamageCalcOver?.Invoke(dmgHandler);
        float finalDamage = dmgHandler.calculateFinalDamage();
        currentHP -= finalDamage;
        dmgHandler.ClearMods();
        TakeDamageGFX?.Invoke();
        if (currentHP <= 0)
        {
            onActorDeath();
            host.OnKill?.Invoke(this);
        }
        ClampHP();
    }

    public void GetHit(Ability m_ability)
    {
        if (effectable)
        {
            foreach (var SE in m_ability.StatusEffects)
            {
                if (m_ability.RollForStatusActivation(SE))
                {
                    RecieveStatusEffects(SE.StatusType);
                }
            }
        }
        TakeDamage(m_ability.DamageHandler);
    }

    public void TakeDamage(DamageHandler dmgHandler)
    {
        onTakeDamage?.Invoke(dmgHandler);
        OnDamageCalcOver?.Invoke(dmgHandler);
        float finalDamage = dmgHandler.calculateFinalDamage();
        currentHP -= finalDamage;
        dmgHandler.ClearMods();
        TakeDamageGFX?.Invoke();
        if (currentHP <= 0)
        {
            onActorDeath();
        }
        ClampHP();
    }
    public void Heal(DamageHandler givenDmg)
    {
        OnRecieveHealth?.Invoke(givenDmg);
        currentHP += givenDmg.calculateFinalDamage();
        ClampHP();
        OnHealGFX?.Invoke();
    }

    public virtual void RecieveForce()
    {

    }

    public void DisableOnInAnim()
    {
        IsInAttackAnim = false;
    }
    public void EnableOnInAnim()
    {
        IsInAttackAnim = true;
    }
}
