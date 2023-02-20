using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossAnim : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        GameManager.Instance.assets.BossAnimator.Play("BossAnim");
    }

}
