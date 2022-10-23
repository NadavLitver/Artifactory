using UnityEngine;

public class ScreenShakeHandler : MonoBehaviour
{
    [SerializeField]
    private float screenShakeForceScaler;
    [SerializeField]
    private float ScreenShakeElapsedTime;
    float _screenshakeForce;
    public void screenShakeBasedOnDamage(DamageHandler givenDmg)
    {
        _screenshakeForce = givenDmg.calculateFinalDamage();
        _screenshakeForce /= 100;
        _screenshakeForce += 1;
        _screenshakeForce = Mathf.Clamp(_screenshakeForce, 0.1f, 5);
        HorizontalShake();
    }

    private void HorizontalShake()
    {
        LeanTween.moveX(gameObject, _screenshakeForce, ScreenShakeElapsedTime).setOnComplete(VerticalShake);
    }
    private void VerticalShake()
    {
        LeanTween.moveY(gameObject, _screenshakeForce, ScreenShakeElapsedTime).setOnComplete(ResetCamShake);
    }

    private void ResetCamShake()
    {
        LeanTween.move(gameObject, Vector3.zero, ScreenShakeElapsedTime);
    }
}
