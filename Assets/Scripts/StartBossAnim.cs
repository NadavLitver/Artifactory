using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossAnim : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.assets.BossAnimator.Play("BossAnim");
    }

}
