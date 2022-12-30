using Cinemachine;
using System.Collections;
using UnityEngine;
public class CinemachineShake : MonoBehaviour
{

    CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin camNoise;
    Coroutine activeRoutine;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        camNoise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        GameManager.Instance.assets.playerActor.OnDealingDamageCalcOver.AddListener(Shake);
    }

    public void Shake(DamageHandler givenDmg)
    {
        if (!ReferenceEquals(camNoise, null))
        {
            camNoise.m_AmplitudeGain = givenDmg.calculateFinalNumberMult() / 100;
            camNoise.m_AmplitudeGain = Mathf.Clamp(camNoise.m_AmplitudeGain, 0, 1);
            camNoise.m_AmplitudeGain *= 2;
            if (!ReferenceEquals(activeRoutine, null))
            {
                StopCoroutine(activeRoutine);
            }
            activeRoutine = StartCoroutine(DecreaseOverTime());
        }
    }

    IEnumerator DecreaseOverTime()
    {
        while (camNoise.m_AmplitudeGain > 0)
        {
            camNoise.m_AmplitudeGain -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        camNoise.m_AmplitudeGain = 0;
        cam.transform.parent.eulerAngles = Vector3.zero;
    }

}
