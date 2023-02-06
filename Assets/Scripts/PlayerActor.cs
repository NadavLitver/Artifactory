using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : Actor
{
    public Animator m_animator;
    [SerializeField] RelicInventory playerRelicInventory;
    [SerializeField] ItemInventory playerItemInventory;

    [SerializeField] private Material normalMat;
    [SerializeField] private Material legendaryMat;
    [SerializeField] private SpriteRenderer mainRenderer;
    [SerializeField] private GameObject legendaryTrailVFX;

    public RelicInventory PlayerRelicInventory { get => playerRelicInventory;}
    public ItemInventory PlayerItemInventory { get => playerItemInventory; set => playerItemInventory = value; }

    private void Start()
    {
        //  OnDealingDamageCalcOver.AddListener(GameManager.Instance.assets.CameraShake.screenShakeBasedOnDamage);
        OnDealDamage.AddListener(PlayerOnHitVFX);
    }
    private void PlayerOnHitVFX(DamageHandler damage,Actor actorHit)
    {
        if (damage.myDmgType == DamageType.fire)
        {
            GameObject vfx = GameManager.Instance.vfxManager.PlayAndGet(VisualEffect.RedHitEffect, actorHit.transform.position + Vector3.up);

        }
        else
        {
            GameObject vfx = GameManager.Instance.vfxManager.PlayAndGet(VisualEffect.WhiteHitEffect, actorHit.transform.position + Vector3.up);

        }
    }
    public override void onActorDeath()
    {
        base.onActorDeath();
        m_animator.Play("Die");
    }

    public void EnableLegendaryActorVFX()
    {
        mainRenderer.material = legendaryMat;
        legendaryTrailVFX.SetActive(true);
    }
    public void DisableLegendaryActorVFX()
    {
        mainRenderer.material = normalMat;
        legendaryTrailVFX.SetActive(false);
    }
}
