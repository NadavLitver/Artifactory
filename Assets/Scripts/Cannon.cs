using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Cannon : Weapon
{
    [SerializeField] ObjectPool bulletPool;
    [SerializeField] Transform muzzle;
    [SerializeField] float chargeDuration;
    [SerializeField] float timeTillFade;
    float loadedTime;

    bool loaded;
    bool charging;
    bool jumped;
    [SerializeField] float jumpForce;

    private void OnEnable()
    {
     
        charging = false;
    }
    protected override void Attack()
    {
        if (!loaded && !charging)
        {
            StartCoroutine(StartCharging());
        }
        CheckBulletFade();
        if (loaded)
        {
            FireBullet();
        }
    }

    IEnumerator StartCharging()
    {
        charging = true;
        yield return new WaitForSecondsRealtime(chargeDuration);
        charging = false;
        loaded = true;
        loadedTime = Time.time;
        Debug.Log("cannon armed");
    }

    public void CheckBulletFade()
    {
        if (Time.time - loadedTime >= timeTillFade)
        {
            loaded = false;
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
        yield return new WaitUntil(() => GameManager.Instance.assets.PlayerController.GetIsGrounded);
        jumped = false;
    }


    private void FireBullet()
    {
        GameObject bullet = bulletPool.GetPooledObject();
        bullet.transform.position = muzzle.position;
       // bullet.CacheDirection(transform.right);
        bullet.gameObject.SetActive(true);
        loaded = false;
        /*m_animator.Play("Attack");*/
    }


}
