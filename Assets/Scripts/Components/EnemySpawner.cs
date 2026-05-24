using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemySpawner : MonoBehaviour
{
    public static Func<Enemy, Vector2, Enemy> _SpawnEnemy;

    [SerializeField] private Enemy SquareEnemy;
    [SerializeField] private Enemy TriangleEnemy;
    [SerializeField] private Enemy HexagonEnemy;

    public void Spawn()
    {
        int randEnemyNum = UnityEngine.Random.Range(1, 4);
        Enemy enemy = (randEnemyNum) switch
        {
            1 => _SpawnEnemy(SquareEnemy, transform.position),
            2 => _SpawnEnemy(TriangleEnemy, transform.position),
            3 => _SpawnEnemy(HexagonEnemy, transform.position),
        };
    }
}
