using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossAnim : MonoBehaviour
{
    [SerializeField] AudioSource m_audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        m_audioSource.Play();
        GameManager.Instance.assets.BossAnimator.Play("BossAnim");
    }

}
