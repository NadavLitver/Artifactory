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

    public void Awake()
    {
        currentHP = maxHP;
        onTakeDamage = new UnityEvent<DamageHandler>();
        m_StatusEffects = new List<StatusEffect>();
        dotRoutines = new List<DOTStatusEffectData>();
    }
    private void ClampHP() => currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    //public virtual void TakeDamage(float damage)
    //{
    //    currentHP -= damage;
    //    Debug.Log(gameObject.name + "Recieved Damage at this amount" + damage + "and now has " + currentHP + " Amount of HP");
    //    onTakeDamage?.Invoke();

    //    if (currentHP < 1)
    //    {
    //        onActorDeath();
    //    }
    //    ClampHP();

    //}
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
                StopRoutineInCaseOfDOT(item);
                item.Unsubscribe();
                return;
            }
        }
    }

    private void StopRoutineInCaseOfDOT(StatusEffect givenEffect)
    {
        foreach (var item in dotRoutines)
        {
            if (item.GetType().Equals(givenEffect.GetType()))
            {
                StopCoroutine(item.dotRoutine);
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
        //RecieveStatusEffects(givenAbility);

        //
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
        Debug.Log(gameObject.name + "has been hit and has " + currentHP + " HP ");
        //onTakeDamage?.Invoke();
    }
    #region statusEffectRoutinesAndFunctions
    public void StartDOT(float time, float intervals, DamageHandler dmg, StatusEffect givenStatus)
    {
        dotRoutines.Add(new DOTStatusEffectData() { dotRoutine = StartCoroutine(TakeDamageOverTime(time, intervals, dmg, givenStatus)), myStatus = givenStatus });
    }


    private IEnumerator TakeDamageOverTime(float time, float intervals, DamageHandler dmg, StatusEffect givenStatus)
    {
        float counter = 0;
        float intervalstime = time / intervals;
        while (counter < time)
        {
            TakeDamage(dmg);
            yield return new WaitForSeconds(intervalstime);
            Debug.Log("yes " + counter);
            counter += intervalstime;
        }
        //end
        RemoveStatusEffect(givenStatus);
    }
    #endregion
    //public virtual void GetHealth(float health)
    //{
    //    onGetHealth?.Invoke();
    //    currentHP += health;
    //    Debug.Log(gameObject.name + "Recieved health at this amount" + health + "and now has " + currentHP + " Amount of HP");

    //    ClampHP();


    //}
}

public class DOTStatusEffectData
{
    public Coroutine dotRoutine;
    public StatusEffect myStatus;
}
