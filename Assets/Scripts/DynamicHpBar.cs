using UnityEngine;
using System.Collections.Generic;
public class DynamicHpBar : MonoBehaviour
{
    Actor actor;
    float maxHp;
    float curHp;

    [SerializeField] float hpThreshold;
    [SerializeField] Transform bar;
    [SerializeField] Transform cubePrefab;
    //cubes amount = maxhp/ threshold
    //cube x width  = threshold/maxhp

    List<Transform> cubesCreated = new List<Transform>();

    float camulativeDmg = 0;
    private void Start()
    {
        actor = GetComponentInParent<Actor>();
        actor.TakeDamageGFX.AddListener(updateValues);
        actor.OnDamageCalcOver.AddListener(UpdateBar);
        updateValues();
        DrawCubes();
    }

    void updateValues()
    {
        maxHp = actor.maxHP;
        curHp = actor.currentHP;
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



    public void UpdateBar(DamageHandler givenDmg)
    {
        camulativeDmg += givenDmg.calculateFinalDamage();
        int amountToRemove = (int)(camulativeDmg / hpThreshold);
        for (int i = 0; i < amountToRemove; i++)
        {
            cubesCreated[cubesCreated.Count- 1].gameObject.SetActive(false);
            cubesCreated.RemoveAt(cubesCreated.Count - 1);
        }
    }


}
