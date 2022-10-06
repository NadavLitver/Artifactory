using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour, IDamagable
{
    public float currentHP;
    public float maxHP;
    public List<StatusEffect> ActorStatusEffects { get => m_StatusEffects; set => m_StatusEffects = value; }
    public UnityEvent<DamageHandler> onTakeDamage { get; set; }
    public UnityEvent<DamageHandler, Actor> OnDealDamage;
    public UnityEvent<Ability, Actor> OnHit;
    public UnityEvent<Actor> OnKill;
    public UnityEvent<StatusEffectEnum> OnApplyStatusEffect;

    public UnityEvent TakeDamageEvent, DeathEvent;

    //  public UnityEvent onTakeDamage, onGetHealth, OnDeath;
    [SerializeReference] private List<StatusEffect> m_StatusEffects;

    public Weapon TempActiveWeapon;

    public bool IsInAttackAnim;


    public void Awake()
    {
        currentHP = maxHP;
        onTakeDamage = new UnityEvent<DamageHandler>();
        m_StatusEffects = new List<StatusEffect>();
        TempActiveWeapon = GetComponentInChildren<Weapon>();
    }
    private void ClampHP() => currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    public virtual void onActorDeath()
    {
        Debug.Log(gameObject.name + "hasDead");
        DeathEvent?.Invoke();
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
                return;
            }
        }
    }
    public void ToggleIsinAnim()
    {
        IsInAttackAnim = !IsInAttackAnim;
    }
    public void GetHit(Ability givenAbility, Actor host)
    {
        foreach (var SE in givenAbility.StatusEffects)
        {
            if (givenAbility.RollForStatusActivation(SE))
            {
                RecieveStatusEffects(SE.StatusType);
                host.OnApplyStatusEffect?.Invoke(SE.StatusType);
            }
        }
        host.OnHit?.Invoke(givenAbility, this);
        //invoking the hit event on the actor that hit me
        TakeDamage(givenAbility.DamageHandler, host);
    }

    public void TakeDamage(DamageHandler dmgHandler, Actor host)
    {
        host.OnDealDamage?.Invoke(dmgHandler, this);
        onTakeDamage?.Invoke(dmgHandler);
        //me taking dmg
        //other guy dealing dmg
        float finalDamage = dmgHandler.calculateFinalDamage();
        currentHP -= finalDamage;
        TakeDamageEvent?.Invoke();
        if (currentHP < 0)
        {
            onActorDeath();
            host.OnKill?.Invoke(this);
        }
        ClampHP();
    }

    public void GetHit(Ability m_ability)
    {
        foreach (var SE in m_ability.StatusEffects)
        {
            if (m_ability.RollForStatusActivation(SE))
            {
                RecieveStatusEffects(SE.StatusType);
            }
        }

        TakeDamage(m_ability.DamageHandler);
    }

    public void TakeDamage(DamageHandler dmgHandler)
    {
        onTakeDamage.Invoke(dmgHandler);
        float finalDamage = dmgHandler.calculateFinalDamage();
        currentHP -= finalDamage;
        TakeDamageEvent?.Invoke();
        if (currentHP < 0)
        {
            onActorDeath();
        }
        ClampHP();
    }
}
