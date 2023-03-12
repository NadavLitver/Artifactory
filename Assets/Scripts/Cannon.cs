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
    //[SerializeField] ParticleSystem cannonGlow;
    [SerializeField] float slowDuration;
    [SerializeField] float gravityScaleOnShoot;

    [SerializeField] private float endEmission;

    private int AttackHash;
    private int MobilityHash;

    Coroutine shootingRoutine;

    public bool ShootFlag;
    private void OnEnable()
    {
        isCharging = false;
       // cannonGlow.gameObject.SetActive(true);
        if (GameManager.Instance.assets.PlayerController.GetIsGrounded)
        {
            ResetJumped();
        }
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
        GameManager.Instance.assets.PlayerController.OnsGroundCheck1.OnGrounded.AddListener(ResetJumped);
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
        //var emission = cannonGlow.emission;
        //emission.rateOverTime = 0f;
        isCharging = true;
        float counter = 0f;
        while (counter < 1)
        {
            counter += Time.deltaTime / chargeDuration;
            //emission.rateOverTime = Mathf.Lerp(0f, endEmission, counter);
            yield return new WaitForEndOfFrame();
        }
      //  emission.rateOverTime = endEmission;
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
            if (!isLoaded && !isCharging)
            {
                StartCoroutine(StartCharging());
            }
        }
    }

    private void ResetJumped()
    {
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
        bullet.SetActive(true);
        ShootFlag = false;
        StartCoroutine(StartCharging());
    }
    private void OnDisable()
    {
        //jumped = false;
        isLoaded = false;
      //  cannonGlow.gameObject.SetActive(false);
        GameManager.Instance.assets.PlayerController.canMove = true;
        GameManager.Instance.assets.PlayerController.ResetGravity();
    }

}
