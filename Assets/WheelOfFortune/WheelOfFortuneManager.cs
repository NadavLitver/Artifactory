using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WheelOfFortuneManager : MonoBehaviour//not a real "Manager"
{
    [SerializeField] float spinDurationMin, SpinDurationMax;
    [SerializeField] float spinAmountMin, SpinAmountMax;
    private float currentAmount => Random.Range(spinAmountMin, SpinAmountMax);
    private float currentDuration => Random.Range(spinDurationMin, SpinDurationMax);
    public UnityEvent<int> OnSpinOverWithWinnerIndex;
    public RectTransform[] sections;
    [SerializeField] int AmountMultiplier;
    [SerializeField] Button SpinButton;
   

    [ContextMenu("Start Random Spin")]
    public void RandomSpin()
    {
        Debug.Log("Current Spin Amount " + currentAmount + " Currnet Spin Duration " + currentDuration);

        LeanTween.rotateZ(gameObject, transform.rotation.z + currentAmount, currentDuration).setOnComplete(() => CheckWinningSection());
    }

    [ContextMenu("Start normal Spin")]
    public void SpinBasedOnResultOfChances()
    {
        SpinButton.enabled = false;
       float result = Random.value;    
        if (inBetween(result, 0, 0.19f))
        {
            Debug.Log("First Win");
            LeanTween.rotateZ(gameObject, transform.rotation.z + (2160 ), currentDuration).setOnComplete(() => CheckWinningSection());

        }
        else if (inBetween(result, 0.19f, 0.38f))
        {
            Debug.Log("Second Win");

            LeanTween.rotateZ(gameObject, transform.rotation.z + (2220 ), currentDuration).setOnComplete(() => CheckWinningSection());

        }
        else if(inBetween(result, 0.38f, 0.57f))
        {
            Debug.Log("Third Win");

            LeanTween.rotateZ(gameObject, transform.rotation.z + (2280 ), currentDuration).setOnComplete(() => CheckWinningSection());

        }
        else if (inBetween(result, 0.57f, 0.76f))
        {
            Debug.Log("Fourth Win");

            LeanTween.rotateZ(gameObject, transform.rotation.z + (2340 ), currentDuration).setOnComplete(() => CheckWinningSection());

        }
        else if (inBetween(result, 0.76f, 0.95f))
        {
            Debug.Log("Fifth Win");

            LeanTween.rotateZ(gameObject, transform.rotation.z + (2400), currentDuration).setOnComplete(() => CheckWinningSection());

        }
        else if (inBetween(result, 0.95f, 1f))
        {
            Debug.Log("Six Win");

            LeanTween.rotateZ(gameObject, transform.rotation.z + (2460), currentDuration).setOnComplete(() => CheckWinningSection());

        }
    }
    public void CheckWinningSection()
    {
        int WinningIndex = 0;
        float highestHeight = sections[0].position.y;
        RectTransform highestRect = sections[0];
        for (int i = 0; i < sections.Length; i++)
        {
            Debug.Log(sections[i].position);
            if(sections[i].position.y > highestHeight)
            {
                WinningIndex = i;
                highestHeight = sections[i].position.y;
                highestRect = sections[i];
            }
        }
        OnSpinOverWithWinnerIndex.Invoke(WinningIndex);
        Debug.Log("Highest Height is " + highestHeight + "Section is" + highestRect.gameObject.name);

    }
   
    bool inBetween(float numToCheck,float min, float max)
    {
        if(numToCheck >= min && numToCheck <= max)
        {
            return true;
        }
        return false;
    }
    private void OnDisable()
    {
        SpinButton.enabled = true;
    }
}
