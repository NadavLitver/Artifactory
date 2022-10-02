using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Cannon : Weapon
{
    [SerializeField] ObjectPool bulletPool;
    [SerializeField] Transform muzzle;
    [SerializeField] float chargeDuration;
    [SerializeField] float timeToLoad;
    [SerializeField] float timeTillFade;
    bool loaded;
    bool charging;

  
    protected override void Attack()
    {
        if (!loaded && !charging)
        {
            StartCoroutine(StartCharging());
            StartCoroutine(FadeBullet());
        }
        else if (loaded)
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
        Debug.Log("cannon armed");
    }

    IEnumerator FadeBullet()
    {
        yield return new WaitUntil(() => loaded);
        yield return new WaitForSecondsRealtime(timeTillFade);
        if (loaded)
        {
            loaded = false;
        }
    }

    private void FireBullet()
    {
        GameObject bullet = bulletPool.GetPooledObject();
        bullet.transform.position = muzzle.position;
       // bullet.CacheDirection(transform.right);
        bullet.gameObject.SetActive(true);
        loaded = false;
        m_animator.Play("Attack");
    }


}
