using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class Weapon : MonoBehaviour
{
    protected Animator m_animator;
    public UnityEvent onHit;
    public Ability m_ability;
    public Ability m_UltimateAbility;
    protected Ability currentAbility;


    private void Awake()
    {
        m_animator = GetComponentInParent<Animator>();
    }
    protected virtual void Attack()
    {
       
    }
    protected virtual void Mobility()
    {

    }
    protected virtual void Ultimate()
    {

    }
    protected virtual void OnWeaponHit(Collider2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnWeaponHit(collision);
        onHit?.Invoke();
    }
    private void OnEnable()
    {
        GameManager.Instance.inputManager.onAttackDown.AddListener(Attack);
        GameManager.Instance.inputManager.onMobilityDown.AddListener(Mobility);
        GameManager.Instance.inputManager.onUltimateDown.AddListener(Ultimate);



    }
    private void OnDisable()
    {
        GameManager.Instance.inputManager.onAttackDown.RemoveListener(Attack);
        GameManager.Instance.inputManager.onMobilityDown.RemoveListener(Mobility);
        GameManager.Instance.inputManager.onUltimateDown.RemoveListener(Ultimate);



    }
}
