using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator m_animator;
    private void Awake()
    {
        m_animator = GetComponentInParent<Animator>();
    }
    protected virtual void Attack()
    {
       
    }
    protected virtual void OnWeaponHit(Collider2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnWeaponHit(collision);
    }
    private void OnEnable()
    {
        GameManager.Instance.inputManager.onAttackDown.AddListener(Attack);

    }
    private void OnDisable()
    {
        GameManager.Instance.inputManager.onAttackDown.RemoveListener(Attack);

    }
}
