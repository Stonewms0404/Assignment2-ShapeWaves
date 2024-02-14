using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public static event Action<bool, int> _SpawnEnemy;
    public static event Action<int> _NextWaveMultiplier;

    private int waveNumber = 0;
    private int enemiesPerWave;
    private float spawnTimer;
    private readonly float betweenWavesTimer = 10.0f;
    private float timerSpawn;
    private float timerWaves;
    private bool isPlayerDead = false;
    private bool canSpawn = false;
    private bool canSpawnBoss = false;
    private bool betweenWaves = false;

    public float waveMultiplier = 1;

    [SerializeField] private GameObject WaveNumberTextObj;
    [SerializeField] private TextMeshProUGUI WaveNumberText;

    private void OnEnable()
    {
        EnemySpawner._EnemySpawned += EnemySpawned;
        Player._HitObject += SetIsPlayerDead;
        Player._NotDead += SetIsPlayerDead;
    }
    private void OnDisable()
    {
        EnemySpawner._EnemySpawned -= EnemySpawned;
        Player._HitObject -= SetIsPlayerDead;
        Player._NotDead -= SetIsPlayerDead;
    }

    private void Start()
    {
        WaveNumberText = WaveNumberTextObj.GetComponent<TextMeshProUGUI>();

        betweenWaves = true;
        timerWaves = 7;
    }

    private void FixedUpdate()
    {
        if (!isPlayerDead)
        {
            if (betweenWaves)
            {
                if (timerWaves <= betweenWavesTimer)
                {
                    timerWaves += Time.deltaTime;
                }
                else
                {
                    betweenWaves = false;
                    NextWave();
                }
            }
            else
            {
                if (waveNumber % 5 == 0)
                {
                    canSpawnBoss = true;
                }

                if (canSpawnBoss)
                {
                    canSpawnBoss = false;
                    SpawnBoss();
                }

                if (timerSpawn <= spawnTimer && !canSpawn)
                {
                    timerSpawn += Time.deltaTime;
                    canSpawn = false;
                }
                else
                {
                    canSpawn = true;
                    ResetTimer();
                }

                if (canSpawn)
                {
                    _SpawnEnemy(canSpawn, UnityEngine.Random.Range(1, 5)); // Spawns the enemy from one of the four spawners
                    canSpawn = false;
                    ResetTimer();
                }
                betweenWaves = enemiesPerWave <= 0;
            }

        }
        else
        {
            ResetTimer();
        }
    }

    private void EnemySpawned(int value)
    {
        --enemiesPerWave;
    }

    private void SpawnBoss()
    {

    }

    private void SetWaveMultiplier()
    {
        waveMultiplier = Math.Clamp(waveMultiplier + .2f, 1, 2.5f);
    }

    private void ResetTimer()
    {
        timerSpawn = 0.0f;
        timerWaves = 0.0f;
        spawnTimer = UnityEngine.Random.Range(3.0f, 5.0f) * (1 / waveMultiplier);
    }

    private void SetEnemiesPerWave()
    {
        enemiesPerWave = (int)(UnityEngine.Random.Range(8, 12) * waveMultiplier);
    }

    private void SetIsPlayerDead(bool playerDead)
    {
        isPlayerDead = playerDead;
    }

    private void NextWave()
    {
        ++waveNumber;
        if (waveNumber == 1)
        {
            _NextWaveMultiplier(waveNumber);
        }

        ResetTimer();
        SetEnemiesPerWave();
        SetWaveMultiplier();
        WaveNumberText.text = "Wave: " + waveNumber;
    }
}
