using Cinemachine;
using System.Collections;
using UnityEngine;
public class CinemachineShake : MonoBehaviour
{

    CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin camNoise;
    Coroutine activeRoutine;
    [SerializeField] private float maxAmplitude;
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
            ClampAmplitude();
            if (!ReferenceEquals(activeRoutine, null))
            {
                StopCoroutine(activeRoutine);
            }
            activeRoutine = StartCoroutine(DecreaseOverTime());
        }
    }

    private void ClampAmplitude()
    {
        camNoise.m_AmplitudeGain = Mathf.Clamp(camNoise.m_AmplitudeGain, 0, maxAmplitude);
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
        ClampAmplitude();
    }

}
