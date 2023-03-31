using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BarMode
{
    Cubic,
    ChipAway
}
public class DynamicHpBar : MonoBehaviour
{
    [SerializeField] Actor actor;
    float maxHp;
    float curHp;
    [SerializeField] bool refill;
    [SerializeField] bool useUi;
    [SerializeField] BarMode mode;
    [SerializeField] float hpThreshold;
    [SerializeField] Transform bar;
    [SerializeField] Transform cubePrefab;
    [SerializeField] Image damageImage;
    [SerializeField] Image hpImage;
    [SerializeField] Transform chipAwayhealthBar;
    [SerializeField] Transform chipAwayDamagedhealthBar;
    Coroutine activeRotineDMG;
    Coroutine activeRotineHeal;
    List<Transform> cubesCreated = new List<Transform>();

    float camulativeDmg = 0;
    float lastChangeCurrentHp = 0;

    private void Start()
    {
        //actor = GetComponentInParent<Actor>();
        actor.TakeDamageGFX.AddListener(updateValues);
        switch (mode)
        {
            case BarMode.Cubic:
                actor.OnDamageCalcOver.AddListener(UpdateCubicBar);
                DrawCubes();
                break;
            case BarMode.ChipAway:
                actor.TakeDamageGFX.AddListener(updateValues);
                actor.TakeDamageGFX.AddListener(ChipAwayBarTakeDmg);
                actor.OnHealGFX.AddListener(updateValues);
                actor.OnHealGFX.AddListener(ChipAwayBarHealDamage);
                break;
            default:
                break;
        }
        StartCoroutine(FlipWhenNegative());
    }

    void updateValues()
    {
        maxHp = actor.maxHP;
        curHp = actor.currentHP;
        EnqueueNextBarChange();
    }

    [ContextMenu("createCubes")]
    void DrawCubes()
    {
        float cubesAmount = (maxHp / hpThreshold);
        int amountToSpawn = (int)(maxHp / hpThreshold);
        float cubeXWidth = hpThreshold / maxHp;
        float startingSpawnPos = -0.4f;
        for (int i = 0; i < amountToSpawn; i++)
        {
            Transform cube = GameManager.Instance.assets.CubePool.GetPooledObject().transform;
            cube.SetParent(bar);
            cube.localScale = new Vector3(cubeXWidth, 1, 1);
            cube.localPosition = new Vector3(startingSpawnPos + (cubeXWidth * i), 0, 0);
            cubesCreated.Add(cube);
            cube.gameObject.SetActive(true);
        }

        if (cubesAmount % 1 != 0)//if the amount of cubes is not whole
        {
            float lastCubeSacleMod = cubesAmount - amountToSpawn;
            float lastCubePosX = (startingSpawnPos + (cubeXWidth * (amountToSpawn - 1)));
            Transform cube = GameManager.Instance.assets.CubePool.GetPooledObject().transform;
            cube.SetParent(bar);
            cube.localScale = new Vector3(cubeXWidth * lastCubeSacleMod, 1, 1);
            cube.localPosition = new Vector3(((cubeXWidth - cube.localScale.x) + lastCubePosX), 0, 0);
            cubesCreated.Add(cube);
            cube.gameObject.SetActive(true);
        }
    }

    public void UpdateCubicBar(DamageHandler givenDmg)
    {
        camulativeDmg = givenDmg.calculateFinalNumberMult();
        int amountToRemove = (int)(camulativeDmg / hpThreshold);
        for (int i = 0; i < amountToRemove; i++)
        {
            cubesCreated[cubesCreated.Count - 1].gameObject.SetActive(false);
            cubesCreated.RemoveAt(cubesCreated.Count - 1);
            if (cubesCreated.Count <= 0 && refill)
            {
                DrawCubes();
            }
        }
    }


    public void EnqueueNextBarChange()
    {
        if (!ReferenceEquals(activeRotineHeal, null))
        {
            StopCoroutine(activeRotineHeal);
        }
        if (!ReferenceEquals(activeRotineDMG, null))
        {
            StopCoroutine(activeRotineDMG);
        }
    }

    public void ChipAwayBarTakeDmg()
    {
        if (curHp <=0 && actor is PlayerActor)
        {
            return;
        }
        float amount = curHp / maxHp;
        if (amount <= 0)
        {
            chipAwayhealthBar.localScale = new Vector3(0, 1, 1);
            activeRotineDMG = StartCoroutine(ChipAwayHpBarDamage());
            return;
        }
        if (useUi)
        {
            hpImage.fillAmount = amount;
        }
        else
        {
            chipAwayhealthBar.localScale = new Vector3(amount, 1, 1);
        }
        activeRotineDMG = StartCoroutine(ChipAwayHpBarDamage());
    }
    public void ChipAwayBarHealDamage()
    {
        if (curHp <= 0 && actor is PlayerActor)
        {
            return;
        }
        float amount = curHp / maxHp;
        if (amount >= maxHp)
        {
            chipAwayhealthBar.localScale = new Vector3(1, 1, 1);
            activeRotineHeal = StartCoroutine(ChipAwayHpBarHealing());
            return;
        }
        if (maxHp <= 0)
        {
            //Debug.Log(actor.name + " max hp is 0 fix");
        }
        if (useUi)
        {
            damageImage.fillAmount = amount;
        }
        else
        {
            chipAwayDamagedhealthBar.localScale = new Vector3(Mathf.Clamp(curHp, 0, maxHp) / maxHp, 1, 1);
        }
        activeRotineHeal = StartCoroutine(ChipAwayHpBarHealing());
    }

    IEnumerator ChipAwayHpBarDamage()
    {
        float counter = 0;
        float uiFillAmount;
        Vector3 sartScaleBack = Vector3.zero;
        Vector3 startScaleFront = Vector3.zero;
        if (!useUi)
        {
            sartScaleBack = chipAwayDamagedhealthBar.localScale;
            startScaleFront = chipAwayhealthBar.localScale;
        }
        while (counter < 1)
        {
            if (useUi)
            {
                uiFillAmount = Mathf.Lerp(damageImage.fillAmount, hpImage.fillAmount, counter);
                damageImage.fillAmount = uiFillAmount;
            }
            else
            {
                Vector3 scaleLerp = Vector3.Lerp(sartScaleBack, startScaleFront, counter);
                chipAwayDamagedhealthBar.localScale = scaleLerp;
            }
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator ChipAwayHpBarHealing()
    {
        float counter = 0;
        float uiFillAmount;
        Vector3 sartScaleBack = Vector3.zero;
        Vector3 startScaleFront = Vector3.zero;
        if (!useUi)
        {
            sartScaleBack = chipAwayDamagedhealthBar.localScale;
            startScaleFront = chipAwayhealthBar.localScale;
        }
        while (counter < 1)
        {
            if (useUi)
            {
                uiFillAmount = Mathf.Lerp(hpImage.fillAmount, damageImage.fillAmount, counter);
                hpImage.fillAmount = uiFillAmount;
            }
            else
            {
                Vector3 scaleLerp = Vector3.Lerp(startScaleFront, sartScaleBack, counter);
                chipAwayhealthBar.localScale = scaleLerp;
            }
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator FlipWhenNegative()
    {
        yield return new WaitUntil(() => transform.parent.transform.localScale.x < 0);
        transform.localScale *= -1;
        StartCoroutine(FlipWhenPositive());
    }

    IEnumerator FlipWhenPositive()
    {
        yield return new WaitUntil(() => transform.parent.transform.localScale.x > 0);
        transform.localScale *= -1;
        StartCoroutine(FlipWhenNegative());
    }
}
