using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AbilityCombo : MonoBehaviour
{
    [SerializeField] List<AbilityComboData> abilities = new List<AbilityComboData>();
    [SerializeField] AbilityComboData currentAbilityCombo;
   [SerializeField] Actor m_actor;
    bool canAttack;
    public List<AbilityComboData> Abilities { get => abilities; set => abilities = value; }
    public AbilityComboData CurrentAbilityData { get => currentAbilityCombo; set => currentAbilityCombo = value; }
    public Ability CurrentAbility { get => currentAbilityCombo.Ability; }
    public bool CanAttack { get => canAttack; set => canAttack = value; }
    [SerializeField, Range(0.1f, 2)] float clickGraceTime;
    float lastAttackTime;
    public UnityEvent OnAttackPerformed;


    private void Start()
    {
        m_actor = GetComponentInParent<Actor>();
        if (ReferenceEquals(m_actor, null))
        {
            Debug.LogError("Ability Combo on" + gameObject.name + " is Null");
        }
        m_actor.IsInAttackAnim = false;
        ResetCombo();
    }
    public void PlayNextAbility()
    {
        if (m_actor.IsInAttackAnim)
        {
            return;
        }
        CheckAbiltyCoolDown();
        if (canAttack)
        {
            PlayAttack();
        }
    }

    public void CheckAbiltyCoolDown()
    {
        if (Time.time - lastAttackTime > currentAbilityCombo.AbilityRecovery + clickGraceTime)//x+y passed = reset
        {
            //if the player pressed after the cooldown + the allowed delay passed since the last attack the combo resets
            ResetCombo();
        }
        else if (Time.time >= lastAttackTime + currentAbilityCombo.AbilityRecovery && Time.time <= lastAttackTime + currentAbilityCombo.AbilityRecovery + clickGraceTime)// after cooldown but before grace end
        {
            SetNextAbility();
        }
    }

    public void PlayAttack()
    {
        OnAttackPerformed?.Invoke();
        canAttack = false;
        lastAttackTime = Time.time;
       // Debug.Log(GetAbilityIndex());
    }

   

    private void SetNextAbility()
    {
        canAttack = true;

        if (currentAbilityCombo == abilities[abilities.Count - 1])
        {
            ResetCombo();
            return;
        }

        for (int i = 0; i < abilities.Count; i++)
        {
            if (currentAbilityCombo == abilities[i])
            {
                currentAbilityCombo = abilities[i + 1];
                return;
            }
        }
    }

    public void ResetCombo()
    {
        currentAbilityCombo = abilities[0];
        canAttack = true;
    }

    public int GetAbilityIndex()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (currentAbilityCombo == abilities[i])
            {
                return i + 1;
            }
        }
        return 0;
    }
  
}

[System.Serializable]
public class AbilityComboData
{
    public Ability Ability;
    public float AbilityRecovery;
    public string AbilityAnimationName;
}
