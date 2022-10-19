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

    private void OnEnable()
    {
        isCharging = false;
    }
    private void Start()
    {
        foreach (var item in bulletPool.pooledObjects)
        {
            Bullet bullet = item.GetComponent<Bullet>();
            bullet.CahceSource(this);
        }
    }

    protected override void Attack()
    {
        if (!isLoaded && !isCharging)
        {
            StartCoroutine(StartCharging());
        }
        CheckBulletFade();
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


    protected override void Mobility()
    {
        RocketJump();
    }

    private void RocketJump()
    {
        if (!jumped)
        {
            Vector2 velocity = GameManager.Instance.assets.PlayerController.GetVelocity;
            GameManager.Instance.assets.PlayerController.ResetVelocity();
            GameManager.Instance.assets.PlayerController.RecieveForce(new Vector2(velocity.x, jumpForce));
            jumped = true;
            StartCoroutine(waitForGrounded());
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
        GameObject bullet = bulletPool.GetPooledObject();
        bullet.transform.position = muzzle.position;
        bullet.gameObject.SetActive(true);
        isLoaded = false;
    }


}
