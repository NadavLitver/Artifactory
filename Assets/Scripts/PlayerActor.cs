using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : Actor
{
    public Animator m_animator;
    [SerializeField] RelicInventory playerRelicInventory;

    public RelicInventory PlayerRelicInventory { get => playerRelicInventory;}

    private void Start()
    {
        OnDealingDamageCalcOver.AddListener(GameManager.Instance.assets.CameraShake.screenShakeBasedOnDamage);
    }

    public override void onActorDeath()
    {
        base.onActorDeath();
        m_animator.Play("Die");
    }
    
}
