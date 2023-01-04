using System.Collections;
using UnityEngine;

public class Cannon : Weapon
{
    [SerializeField] ObjectPool bulletPool;
    [SerializeField] Transform muzzle;
    [SerializeField] float chargeDuration;
    [SerializeField] float timeTillFade;
    float loadedTime;
    bool isLoaded;
    bool isCharging;
    bool jumped;
    [SerializeField] float jumpForce;
    [SerializeField] Light cannonGlow;
    [SerializeField] float slowDuration;
    [SerializeField] float gravityScaleOnShoot;
    private int AttackHash;
    private int MobilityHash;

    Coroutine shootingRoutine;

    public bool ShootFlag;
    private void OnEnable()
    {
        isCharging = false;
        StartCoroutine(StartCharging());

    }
    private void Start()
    {
        AttackHash = Animator.StringToHash("Attack");
        MobilityHash = Animator.StringToHash("Mobility");
        foreach (var item in bulletPool.pooledObjects)
        {
            Bullet bullet = item.GetComponent<Bullet>();
            bullet.CahceSource(this);
        }
    }

    protected override void Attack()
    {
        if (isLoaded)
        {
            FireBullet();
        }
    }

    IEnumerator StartCharging()
    {
        isCharging = true;
        yield return new WaitForSecondsRealtime(chargeDuration);
        isCharging = false;
        isLoaded = true;
        loadedTime = Time.time;
        Debug.Log("cannon armed");
    }

    public void CheckBulletFade()
    {
        if (Time.time - loadedTime >= timeTillFade)
        {
            isLoaded = false;
        }
    }


    public override void Mobility()
    {
        RocketJump();

    }

    private void RocketJump()
    {
        if (!jumped)
        {
            m_animator.SetTrigger(MobilityHash);
            GameManager.Instance.vfxManager.Play(VisualEffect.CannonJumpEffect, GameManager.Instance.assets.PlayerController.JumpEffectPoint.position);
            Vector2 velocity = GameManager.Instance.assets.PlayerController.GetVelocity;
            GameManager.Instance.assets.PlayerController.ResetVelocity();
            GameManager.Instance.assets.PlayerController.RecieveForce(new Vector2(velocity.x, jumpForce));
            StartCoroutine(GameManager.Instance.assets.PlayerController.TogglePlayingTraversal());

            jumped = true;
            GameManager.Instance.assets.PlayerController.StartCoroutine(GameManager.Instance.assets.PlayerController.JumpApexWait());
            StartCoroutine(waitForGrounded());
            if (!isLoaded && !isCharging)
            {
                StartCoroutine(StartCharging());
            }
        }
    }

    IEnumerator waitForGrounded()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => GameManager.Instance.assets.PlayerController.GetIsGrounded);
        jumped = false;
    }


    private void FireBullet()
    {
        m_animator.SetTrigger(AttackHash);
        if (!ReferenceEquals(shootingRoutine, null))
        {
            StopCoroutine(shootingRoutine);
        }
        shootingRoutine = StartCoroutine(IEFireBullet());
    }
    public IEnumerator IEFireBullet()
    {
        isLoaded = false;
        yield return new WaitUntil(() => ShootFlag);
        StartCoroutine(GameManager.Instance.assets.PlayerController.FreezePlayerForDuration(slowDuration, gravityScaleOnShoot));
        GameObject bullet = bulletPool.GetPooledObject();
        bullet.transform.position = muzzle.position;
        if (!GameManager.Instance.assets.PlayerController.GetIsLookingRight)
        {
            bullet.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            bullet.transform.localScale = Vector3.one;
        }
        bullet.gameObject.SetActive(true);
        ShootFlag = false;
        StartCoroutine(StartCharging());
    }
    private void OnDisable()
    {
        jumped = false;
        isLoaded = false;
        GameManager.Instance.assets.PlayerController.canMove = true;
        GameManager.Instance.assets.PlayerController.ResetGravity();
    }

}
