using System.Collections;
using UnityEngine;

public class TutorialFramePlayer : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] spriteRenderers;

    bool loopStarted;
    [SerializeField] float timeToFadeIn;
    public bool isActive;
    public AnimationCurve FadeCurve;
    public bool StartActive;

    private void Start()
    {
        loopStarted = false;
        ResetAllFrames();
        if (StartActive)
        {
            CallPlayFrames();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (loopStarted)
            return;
        CallPlayFrames();
    }

    [ContextMenu("Play Frames")]
    public void CallPlayFrames() => StartCoroutine(FadeInFrames());

    private IEnumerator FadeInFrames()
    {
        loopStarted = true;
        while (isActive)
        {
            
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                if(i  != 0)
                {
                    StartCoroutine(FadeFrame(spriteRenderers[i - 1], new Color(0, 0, 0, 0)));
                }
                yield return FadeFrame(spriteRenderers[i], new Color(0, 0, 0, 1));
                if(i == spriteRenderers.Length - 1)
                {
                    yield return FadeFrame(spriteRenderers[i], new Color(0, 0, 0, 0));

                }
                yield return new WaitForEndOfFrame();
            }

        }

    }
    private IEnumerator FadeFrame(SpriteRenderer spriteRenderer, Color Goal)
    {
        float counter = 0;
        float lerpData = counter / timeToFadeIn;
        Color Current = spriteRenderer.color;
        while (lerpData < timeToFadeIn)
        {
            spriteRenderer.color = Color.Lerp(Current, Goal, FadeCurve.Evaluate(lerpData));
            counter += Time.deltaTime;
            lerpData = counter / timeToFadeIn;
            yield return new WaitForEndOfFrame();
        }
    }
    void ResetAllFrames()
    {
        foreach (var renderer in spriteRenderers)
        {
            renderer.color = new Color(0, 0, 0, 0);
        }
    }
}
