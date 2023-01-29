using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour, IDamagable
{
    public float currentHP;
    public float maxHP;
    [SerializeField] float baseCritHit;
    [SerializeField] float baseCritDamage;
    DamageHandler critHit;
    DamageHandler critDamage;
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
    /// invoked when this actor deals critical damage
    /// </summary>
    public UnityEvent<DamageHandler> OnDealCriticalDamage;
    /// <summary>
    /// invoked when this actor takes critical damage
    /// </summary>
    public UnityEvent<DamageHandler> OnTakeCriticalDamage;
    /// <summary>
    /// invoked when this actor is hit 
    /// </summary>
    public UnityEvent<Ability> OnGetHit;
    /// <summary>
    /// invoked when this actor kills another one
    /// </summary>
    public UnityEvent<Actor, DamageHandler> OnKill;
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
    /// <summary>
    /// Invoked when this actor lands an attack with a melee weapon
    /// </summary>
    public UnityEvent<Ability> OnHitMeleeAttack;
    /// <summary>
    /// Invoked when this actor lands an attack with a ranged weapon
    /// </summary>
    public UnityEvent<Ability> OnHitRangedAttack;
    /// <summary>
    /// Invoked when this actor is hit by a ranged attack
    /// </summary>
    public UnityEvent<Ability> OnGetHitByRangedAttack;
    /// <summary>
    /// Invoked when this actor is hit by a Melee attack
    /// </summary>
    public UnityEvent<Ability> OnGetHitByMeleeAttack;
    public UnityEvent TakeDamageGFX, OnDeath;
    public UnityEvent OnHealGFX;

    //  public UnityEvent onTakeDamage, onGetHealth, OnDeath;
    [SerializeField] private List<StatusEffect> m_StatusEffects;

    private bool hittable;

    public bool IsInAttackAnim;

    [SerializeField] protected bool effectable;

    public void Awake()
    {
        currentHP = maxHP;
        onTakeDamage = new UnityEvent<DamageHandler>();
        m_StatusEffects = new List<StatusEffect>();
        SetUpCrits();
        hittable = true;
    }

    void SetUpCrits()
    {
        critHit = new DamageHandler() { amount = baseCritHit };
        critDamage = new DamageHandler() { amount = baseCritDamage + GameManager.Instance.DamageManager.BaseCritDamage + 1 };
    }

    private void ClampHP() => currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    public virtual void onActorDeath()
    {
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
            if (item.GetType() == (givenEffect.GetType()))
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
        //
        //return
        if (!hittable)
            return;

        if (givenAbility.IsMelee)
        {
            StartCoroutine(HitCooldown());
        }

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
        if (host.CalcCrit(givenAbility))//checking for crit hit
        {
            //adding damage mod
            host.OnDealCriticalDamage?.Invoke(givenAbility.DamageHandler);
            OnTakeCriticalDamage?.Invoke(givenAbility.DamageHandler);
            givenAbility.DamageHandler.AddModifier(host.CalcCritDamage(givenAbility));
        }
        OnGetHit?.Invoke(givenAbility);
        if (givenAbility.IsMelee)
        {
            OnGetHitByMeleeAttack?.Invoke(givenAbility);
            host.OnHitMeleeAttack?.Invoke(givenAbility);
        }
        if (givenAbility.IsRanged)
        {
            OnGetHitByRangedAttack?.Invoke(givenAbility);
            host.OnHitRangedAttack?.Invoke(givenAbility);
        }
        //invoking the hit event on the actor that hit me
        TakeDamage(givenAbility.DamageHandler, host);
    }

    public void TakeDamage(DamageHandler dmgHandler, Actor host)
    {
        host.OnDealDamage?.Invoke(dmgHandler, this);
        onTakeDamage?.Invoke(dmgHandler);
        //
        OnDamageCalcOver?.Invoke(dmgHandler);
        host.OnDealingDamageCalcOver?.Invoke(dmgHandler);
        float finalDamage = dmgHandler.calculateFinalNumberMult();
        currentHP -= finalDamage;
        TakeDamageGFX?.Invoke();
        Debug.Log(gameObject.name + "Took Damage");
        if (currentHP <= 0)
        {
            onActorDeath();
            host.OnKill?.Invoke(this, dmgHandler);
        }
        dmgHandler.ClearMods();
        ClampHP();
    }

    public void GetHit(Ability m_ability)
    {
        if (!hittable)
            return;

        if (m_ability.IsMelee)
        {
            StartCoroutine(HitCooldown());
        }
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
        float finalDamage = dmgHandler.calculateFinalNumberMult();
        currentHP -= finalDamage;
        TakeDamageGFX?.Invoke();
        if (currentHP <= 0)
        {
            onActorDeath();
        }
        dmgHandler.ClearMods();
        ClampHP();
    }
    public void Heal(DamageHandler givenDmg)
    {
        OnRecieveHealth?.Invoke(givenDmg);
        currentHP += givenDmg.calculateFinalNumberMult();
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

    public bool CalcCrit(Ability givenAbility)
    {
        critHit.AddModifier(givenAbility.CritHitChace);
        float crit = critHit.calculateFinalNumberAdd();
        critHit.ClearMods();
        if (Random.Range(1, 100) <= crit * 100)
        {
            Debug.Log("Crit!");
            return true;
        }
        return false;
    }

    public float CalcCritDamage(Ability givenAbility)
    {
        critDamage.AddModifier(givenAbility.CritHitDamage);
        float totalCritDamage = critDamage.calculateFinalNumberAdd();
        critDamage.ClearMods();
        return totalCritDamage;
    }
    private IEnumerator HitCooldown()
    {
        if (!hittable)
            yield break;

        hittable = false;
        yield return new WaitForEndOfFrame();
        hittable = true;

    }
}
