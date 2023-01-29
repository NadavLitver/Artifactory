using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatRoom : Room
{
    [Header("Always Have more spawn points than enemies in wave")]
    [SerializeField] List<WaveList> waves;
    [Header("Spawn points refrencing is automatic, do not drag them here. its just exposed for debugging")]
    [SerializeField] SpawnPoint[] spawnPointsForWaves;
    [SerializeField] DropChest m_dropChest;
    Collider2D dropChestCollider;
    private List<Actor> SpawnedEnemies = new List<Actor>();
    public UnityEvent OnSpawnedEnemiesDead;
    public UnityEvent AllWavesOver;
    int waveIndex;
    [SerializeField] GameObject[] ExitClosers;
    [SerializeField] Cinemachine.CinemachineVirtualCamera m_VCam;
    private void Start()
    {
        spawnPointsForWaves = GetComponentsInChildren<SpawnPoint>();
        m_VCam.Priority = 0;
        waveIndex = waves.Count-1;
        OnSpawnedEnemiesDead.AddListener(SpawnEnemies);
        ShuffleWaves();
        ShuffleSpawnPoint();
        AllWavesOver.AddListener(OnAllWavesOver);
        m_dropChest.onTakeDamage.AddListener(StartEvent);
        dropChestCollider = m_dropChest.GetComponent<Collider2D>();
        TurnOffOnExitClosers(false);

    }
    public void OnAllWavesOver()
    {
        Debug.Log("WavesDone");
        dropChestCollider.enabled = true;
        TurnOffOnExitClosers(false);
        m_VCam.Priority = 0;

    }
    private void StartEvent(DamageHandler damageHandler)
    {
        m_VCam.Priority = GameManager.Instance.assets.mainVCam.Priority + 10;
        damageHandler.AddModifier(0);
        SpawnEnemies();
        dropChestCollider.enabled = false;
        m_dropChest.onTakeDamage.RemoveListener(StartEvent);
        TurnOffOnExitClosers(true);
    }

    private void TurnOffOnExitClosers(bool isOn)
    {
        foreach (GameObject go in ExitClosers)
        {
            go.SetActive(isOn);
        }
    }

    public void ShuffleWaves()
    {
        int n = waves.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            WaveList value = waves[k];
            waves[k] = waves[n];
            waves[n] = value;
        }
    }
    public void ShuffleSpawnPoint()
    {
        int n = spawnPointsForWaves.Length;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            SpawnPoint value = spawnPointsForWaves[k];
            spawnPointsForWaves[k] = spawnPointsForWaves[n];
            spawnPointsForWaves[n] = value;
        }
    }
    [ContextMenu("StartSpawning")]
    public void SpawnEnemies()
    {
        Debug.Log("Spawn Enemies Reached");

        if (waveIndex < 0)
        {
            AllWavesOver?.Invoke();
            return;
        }
       

        if (spawnPointsForWaves.Length < waves[waveIndex].Wave.Count)
        {
            Debug.Log("Too much enemies not enough spawnPoints");
            return;
        }

        for (int i = 0; i < waves[waveIndex].Wave.Count; i++)
        {
            // this line spawns
            GameObject GoToSpawn = spawnPointsForWaves[i].SpawnAndGetObject(waves[waveIndex].Wave[i]);
            Actor currentEnemy = GoToSpawn.GetComponent<Actor>();
            if (currentEnemy is null)
            {
                currentEnemy = GoToSpawn.GetComponentInChildren<Actor>();
            }
            SpawnedEnemies.Add(currentEnemy);
            currentEnemy.OnDeath.AddListener(() => SpawnedEnemies.Remove(currentEnemy));
            currentEnemy.OnDeath.AddListener(() => CheckWaveEnemiesDead());

        }
        waveIndex--;
    }
    public bool CheckWaveEnemiesDead()
    {
        if (SpawnedEnemies.Count == 0)
        {
            OnSpawnedEnemiesDead?.Invoke();
            return true;
        }
        return false;

    }
    [ContextMenu("KillWaves")]
    public void KillSpawnedEnemy()
    {
        int amount = SpawnedEnemies.Count;
        for (int i = 0; i < amount; i++)
        {
            DamageHandler currentDmgHandler = new DamageHandler();
            currentDmgHandler.amount = SpawnedEnemies[0].currentHP;
            SpawnedEnemies[0].TakeDamage(currentDmgHandler);
        }
       

    }
}
[Serializable]
class WaveList
{
    [SerializeField] List<GameObject> m_wave;
    public List<GameObject> Wave { get => m_wave; }
}
