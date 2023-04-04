using UnityEngine;

public class PlayerActor : Actor
{
    public Animator m_animator;
    [SerializeField] RelicInventory playerRelicInventory;
    [SerializeField] ItemInventory playerItemInventory;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private Material normalMat;
    [SerializeField] private Material legendaryMat;
    [SerializeField] private SpriteRenderer mainRenderer;
    [SerializeField] private GameObject legendaryTrailVFX;

    public RelicInventory PlayerRelicInventory { get => playerRelicInventory; }
    public ItemInventory PlayerItemInventory { get => playerItemInventory; }
    public WeaponManager WeaponManager { get => weaponManager; }

    private void Start()
    {
        //  OnDealingDamageCalcOver.AddListener(GameManager.Instance.assets.CameraShake.screenShakeBasedOnDamage);
        OnDealDamage.AddListener(PlayerOnHitVFX);
        OnHealGFX.AddListener(OnHealSfx);
        TakeDamageGFX.AddListener(OnTakeDamageSfx);
    }
    private void PlayerOnHitVFX(DamageHandler damage, Actor actorHit)
    {
        if (damage.myDmgType == DamageType.critical)
        {
             GameManager.Instance.vfxManager.PlayAndGet(VisualEffect.RedHitEffect, actorHit.transform.position + Vector3.up);

        }
        else
        {
             GameManager.Instance.vfxManager.PlayAndGet(VisualEffect.WhiteHitEffect, actorHit.transform.position + Vector3.up);

        }
    }
    public override void onActorDeath()
    {
        base.onActorDeath();
        m_animator.Play("Die");
    }
    public void OnHealSfx() => SoundManager.Play(SoundManager.Sound.PlayerHealed, GameManager.Instance.assets.PlayerController.AudioSource);
    public void OnTakeDamageSfx() => SoundManager.Play(SoundManager.Sound.PlayerTakeDamage, GameManager.Instance.assets.PlayerController.AudioSource);

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
