using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AbilityCombo : MonoBehaviour
{
    [SerializeField] List<AbilityComboData> abilities = new List<AbilityComboData>();
    [SerializeField] AbilityComboData currentAbilityCombo;
    bool canAttack;
    public List<AbilityComboData> Abilities { get => abilities; set => abilities = value; }
    public AbilityComboData CurrentAbilityData { get => currentAbilityCombo; set => currentAbilityCombo = value; }
    public Ability CurrentAbility { get => currentAbilityCombo.Ability; }
    public bool CanAttack { get => canAttack; set => canAttack = value; }
    Coroutine routine;
    bool extraInputCahced;
    [SerializeField, Range(0.1f, 1)] float clickGraceTime;
    float lastAttackTime;

    public UnityEvent OnAttackPerformed;


    private void Start()
    {
        ResetCombo();
    }

    public void PlayNextAbility()
    {
        if (canAttack)
        {
            CheckAbiltyCoolDown();
            routine = StartCoroutine(ExecuteAbility());
        }
    }

    public void CheckAbiltyCoolDown()
    {
        if (Time.time - lastAttackTime > currentAbilityCombo.AbilityRecovery + clickGraceTime)
        {
            //if the player pressed after the cooldown + the allowed delay passed since the last attack the combo resets
            ResetCombo();
        }
    }
    
    IEnumerator ExecuteAbility()
    {
        OnAttackPerformed?.Invoke();
        canAttack = false;
        lastAttackTime = Time.time;
        Debug.Log(GetAbilityIndex());
        yield return new WaitForSecondsRealtime(currentAbilityCombo.AbilityRecovery);
        SetNextAbility();
        canAttack = true;
    }


    private void Attack()
    {
        canAttack = false;
        lastAttackTime = Time.time;
    }


    private void SetNextAbility()
    {
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
