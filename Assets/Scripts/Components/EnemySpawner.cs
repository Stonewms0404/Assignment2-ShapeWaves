using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemySpawner : MonoBehaviour
{
    public static event Action<int> _EnemySpawned;

    [SerializeField] private GameObject SquareEnemy;
    [SerializeField] private GameObject TriangleEnemy;
    [SerializeField] private GameObject HexagonEnemy;

    [SerializeField] private int spawnerNumber;
    private bool canSpawnEnemy = false;
    private bool isPlayerDead = false;

    private void OnEnable()
    {
        Player._HitObject += SetIsPlayerDead;
        Player._NotDead += SetIsPlayerDead;
        Waves._SpawnEnemy += SetCanSpawnEnemy;
    }
    private void OnDisable()
    {
        Player._HitObject -= SetIsPlayerDead;
        Player._NotDead -= SetIsPlayerDead;
        Waves._SpawnEnemy -= SetCanSpawnEnemy;
    }

    private void SetIsPlayerDead(bool playerDead)
    {
        isPlayerDead = playerDead;
    }

    public void SetCanSpawnEnemy(bool canSpawn, int selectedSpawner)
    {
        if (selectedSpawner == spawnerNumber)
        {
            canSpawnEnemy = canSpawn;
        }
    }

    private void Update()
    {
        if (canSpawnEnemy && !isPlayerDead)
        {
            canSpawnEnemy = false;
            Spawn();
        }
    }

    public void Spawn()
    {
        int randEnemyNum = UnityEngine.Random.Range(1, 4);
        Vector2 spawnPosition = new(transform.position.x + UnityEngine.Random.Range(1, 1000), transform.position.y + UnityEngine.Random.Range(1, 1000));
        switch (randEnemyNum)
        {
            case 1:
                Instantiate(SquareEnemy, transform);
                break;
            case 2:
                Instantiate(TriangleEnemy, spawnPosition, Quaternion.identity);
                break;
            case 3:
                Instantiate(HexagonEnemy, transform);
                break;
        }
        _EnemySpawned(1);
    }
}