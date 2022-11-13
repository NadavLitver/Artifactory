using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] BarMode mode;
    [SerializeField] float hpThreshold;
    [SerializeField] Transform bar;
    [SerializeField] Transform cubePrefab;
    [SerializeField] bool refill;

    [SerializeField] Transform chipAwayhealthBar;
    [SerializeField] Transform chipAwayDamagedhealthBar;
    Coroutine activeRotineDMG;
    Coroutine activeRotineHeal;
    //cubes amount = maxhp/ threshold
    //cube x width  = threshold/maxhp

    List<Transform> cubesCreated = new List<Transform>();

    float camulativeDmg = 0;
    float lastChangeCurrentHp =0;

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
        float amount = curHp / maxHp;
        if (amount <= 0)
        {
            chipAwayhealthBar.localScale = new Vector3(0, 1, 1);
            activeRotineDMG = StartCoroutine(ChipAwayHpBarDamage());
            return;
        }
        chipAwayhealthBar.localScale = new Vector3(amount, 1, 1);
        activeRotineDMG = StartCoroutine(ChipAwayHpBarDamage());
    }
    public void ChipAwayBarHealDamage()
    {
        float amount = curHp / maxHp;
        if (amount >= maxHp)
        {
            chipAwayhealthBar.localScale = new Vector3(1, 1, 1);
            activeRotineHeal = StartCoroutine(ChipAwayHpBarHealing());

            return;
        }
        chipAwayDamagedhealthBar.localScale = new Vector3(curHp / maxHp, 1, 1);
        activeRotineHeal = StartCoroutine(ChipAwayHpBarHealing());
    }

    IEnumerator ChipAwayHpBarDamage()
    {
        float counter = 0;
        Vector3 sartScaleBack = chipAwayDamagedhealthBar.localScale;
        Vector3 startScaleFront = chipAwayhealthBar.localScale;
        while (counter < 1)
        {
            Vector3 scaleLerp = Vector3.Lerp(sartScaleBack, startScaleFront, counter);
            chipAwayDamagedhealthBar.localScale = scaleLerp;
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }        
    }
    IEnumerator ChipAwayHpBarHealing()
    {
        float counter = 0;
        Vector3 sartScaleBack = chipAwayDamagedhealthBar.localScale;
        Vector3 startScaleFront = chipAwayhealthBar.localScale;
        while (counter < 1)
        {
            Vector3 scaleLerp = Vector3.Lerp(startScaleFront, sartScaleBack, counter);
            chipAwayhealthBar.localScale = scaleLerp;
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    //while losing hp green bar is set to a certain fill, red bar is dragged to chase it
    //while restoring hp red bar is set to a certain fill, green bar is dragged to chase it 
    //if restoring hp while the red bar is being dragged - stop dragging it and set it to a pos instead
    //if taking dmg while the green bar is being dragged - stop dragging it and set it to the pos instead
    //if taking damage and healing and the same time - the second effect is given priority
    IEnumerator FlipWhenNegative()
    {
        yield return new WaitUntil(()=>transform.parent.transform.localScale.x < 0);
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
