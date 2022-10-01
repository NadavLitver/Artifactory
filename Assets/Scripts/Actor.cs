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

    public UnityEvent TakeDamageEvent, DeathEvent;

    List<DOTStatusEffectData> dotRoutines;

    //  public UnityEvent onTakeDamage, onGetHealth, OnDeath;
    [SerializeReference] private List<StatusEffect> m_StatusEffects;

    public Weapon TempActiveWeapon;

    public void Awake()
    {
        currentHP = maxHP;
        onTakeDamage = new UnityEvent<DamageHandler>();
        m_StatusEffects = new List<StatusEffect>();
        dotRoutines = new List<DOTStatusEffectData>();
        TempActiveWeapon = GetComponentInChildren<Weapon>();
    }
    private void ClampHP() => currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    public virtual void onActorDeath()
    {
        Debug.Log(gameObject.name + "hasDead");
        DeathEvent?.Invoke();
    }

    public void RecieveStatusEffects(StatusEffect givenStatusEffect)
    {
        foreach (StatusEffect SE in ActorStatusEffects)
        {
            if (SE.GetType().Equals(givenStatusEffect.GetType()))
            {
                //duplicate type already exists
                SE.Reset();
                return;
            }
        }
        ActorStatusEffects.Add(givenStatusEffect);
        givenStatusEffect.cacheHost(this);

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
    public void GetHit(Ability givenAbility)
    {
        foreach (var SE in givenAbility.StatusEffects)
        {
            if (givenAbility.RollForStatusActivation(SE))
            {
                RecieveStatusEffects(SE.myStatus);
            }
        }
       
        TakeDamage(givenAbility.DamageHandler);
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

public class DOTStatusEffectData
{
    public Coroutine dotRoutine;
    public StatusEffect myStatus;
}
