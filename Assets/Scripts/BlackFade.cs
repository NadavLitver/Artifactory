using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : MonoBehaviour
{
    Image m_image;
    [SerializeField] AnimationCurve fadeToBlackCurve;
    [SerializeField] AnimationCurve fadeFromBlackCurve;
  
    private void Start()
    {
        m_image = GetComponent<Image>();
        if(!GameManager.Instance.isTutorial)
          GameManager.Instance.assets.playerActor.OnDeath.AddListener(FadeToBlack);
        FadeFromBlack();
    }
    public void FadeToBlack()
    {
        StartCoroutine(LerpAlpha(1, fadeToBlackCurve));
    }
    public Coroutine GetFadeToBlackRoutine()
    {
      return StartCoroutine(LerpAlpha(1, fadeToBlackCurve));
    }
    public void FadeFromBlack()
    {
        StartCoroutine(LerpAlpha(0, fadeFromBlackCurve));

    }
    IEnumerator LerpAlpha(float goal, AnimationCurve curve)
    {
        float counter = 0;
        float startingAlpha = m_image.color.a;
        while (counter < 1)
        {
            m_image.color = new Color(0, 0, 0, Mathf.Lerp(startingAlpha, goal, curve.Evaluate(counter)));
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GameManager.Instance.assets.mobileControls.SetActive(true);
    }
    
}
