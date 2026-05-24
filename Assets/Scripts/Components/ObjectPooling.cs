using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] GameObject enemyPoolObject;
    [SerializeField] GameObject yellowEnemyBulletPoolObject;
    [SerializeField] GameObject triangleEnemyBulletPoolObject;
    [SerializeField] GameObject playerBulletPoolObject;

    List<Enemy> enemyPool;
    List<EnemyBullet> yellowEnemyBulletPool;
    List<EnemyBullet> triangleEnemyBulletPool;
    List<PlayerBullet> playerBulletPool;

    const int MAX_ENEMIES = 50;
    const int MAX_ENEMY_BULLETS = 100;
    const int MAX_PLAYER_BULLETS = 10000;

    private void OnEnable()
    {
        enemyPool = new();
        yellowEnemyBulletPool = new();
        triangleEnemyBulletPool = new();
        playerBulletPool = new();

        EnemySpawner._SpawnEnemy += GetEnemy;
        TriangleAI._Shoot += GetTriangleEnemyBullet;
        HexagonAI._Shoot += GetYellowEnemyBullet;
        PlayerShooting._Shoot += GetPlayerBullet;

        Enemy._Death += ReleaseEnemy;
        EnemyBullet._Death += ReleaseEnemyBullet;
        PlayerBullet._Hit += ReleasePlayerBullet;
        Player._HitObject += ReleaseAll;
    }

    private void OnDisable()
    {
        EnemySpawner._SpawnEnemy -= GetEnemy;
        TriangleAI._Shoot -= GetTriangleEnemyBullet;
        HexagonAI._Shoot -= GetYellowEnemyBullet;
        PlayerShooting._Shoot -= GetPlayerBullet;

        Enemy._Death -= ReleaseEnemy;
        EnemyBullet._Death -= ReleaseEnemyBullet;
        PlayerBullet._Hit -= ReleasePlayerBullet;
        Player._HitObject -= ReleaseAll;
    }


    #region Gets
    Enemy GetEnemy(Enemy enemy, Vector2 position)
    {
        // Create a new pool if needed.
        enemyPool ??= new();

        // Find if there is an open slot within the pool.
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (enemyPool[i] != null)
            {
                if (!enemyPool[i].gameObject.activeInHierarchy && enemyPool[i].enemyType == enemy.enemyType)
                {
                    enemyPool[i].gameObject.SetActive(true);
                    enemyPool[i].transform.position = position;
                    return enemyPool[i];
                }
            }
        }

        enemy = Instantiate(enemy, enemyPoolObject.transform);
        enemy.transform.position = position;

        // If no open spot has been found, spawn an object outside of the pool.
        if (enemyPool.Count >= MAX_ENEMIES)
        {
            return enemy;
        }
        // Otherwise, create a new object for the pool.
        enemyPool.Add(enemy);
        return enemy;
    }
    EnemyBullet GetYellowEnemyBullet(EnemyBullet bullet, Vector2 position)
    {
        // If the pool is empty or has not been created.
        if (yellowEnemyBulletPool == null || yellowEnemyBulletPool.Count == 0)
        {
            yellowEnemyBulletPool = new();
            bullet = Instantiate(bullet, yellowEnemyBulletPoolObject.transform);
            bullet.transform.position = position;
            yellowEnemyBulletPool.Add(bullet);
            return bullet;
        }

        // Find if there is an open slot within the pool.
        for (int i = 0; i < yellowEnemyBulletPool.Count; i++)
        {
            if (yellowEnemyBulletPool[i] != null)
            {
                if (!yellowEnemyBulletPool[i].gameObject.activeInHierarchy)
                {
                    yellowEnemyBulletPool[i].gameObject.SetActive(true);
                    yellowEnemyBulletPool[i].transform.position = position;
                    return yellowEnemyBulletPool[i];
                }
            }
            else
            {
                break;
            }
        }

        // If no open spot has been found, spawn an object outside of the pool.
        if (yellowEnemyBulletPool.Count >= MAX_ENEMY_BULLETS)
        {
            if (yellowEnemyBulletPool[^1] != null)
            {
                bullet = Instantiate(bullet, yellowEnemyBulletPoolObject.transform);
                bullet.transform.position = position;
                return bullet;
            }
        }
        // Otherwise, create a new object for the pool.
        bullet = Instantiate(bullet, yellowEnemyBulletPoolObject.transform);
        bullet.transform.position = position;
        yellowEnemyBulletPool.Add(bullet);
        return bullet;
    }
    EnemyBullet GetTriangleEnemyBullet(EnemyBullet bullet, Vector2 position)
    {
        // If the pool is empty or has not been created.
        if (triangleEnemyBulletPool == null || triangleEnemyBulletPool.Count == 0)
        {
            triangleEnemyBulletPool = new();
            bullet = Instantiate(bullet, triangleEnemyBulletPoolObject.transform);
            bullet.transform.position = position;
            triangleEnemyBulletPool.Add(bullet);
            return bullet;
        }

        // Find if there is an open slot within the pool.
        for (int i = 0; i < triangleEnemyBulletPool.Count; i++)
        {
            if (triangleEnemyBulletPool[i] != null)
            {
                if (!triangleEnemyBulletPool[i].gameObject.activeInHierarchy)
                {
                    triangleEnemyBulletPool[i].gameObject.SetActive(true);
                    triangleEnemyBulletPool[i].transform.position = position;
                    return triangleEnemyBulletPool[i];
                }
            }
            else
            {
                break;
            }
        }

        // If no open spot has been found, spawn an object outside of the pool.
        if (triangleEnemyBulletPool.Count >= MAX_ENEMY_BULLETS)
        {
            if (triangleEnemyBulletPool[^1] != null)
            {
                bullet = Instantiate(bullet, triangleEnemyBulletPoolObject.transform);
                bullet.transform.position = position;
                return bullet;
            }
        }
        // Otherwise, create a new object for the pool.
        bullet = Instantiate(bullet, playerBulletPoolObject.transform);
        bullet.transform.position = position;
        triangleEnemyBulletPool.Add(bullet);
        return bullet;
    }
    PlayerBullet GetPlayerBullet(PlayerBullet bullet, Vector2 position)
    {
        // If the pool is empty or has not been created.
        if (playerBulletPool == null || playerBulletPool.Count == 0)
        {
            playerBulletPool = new();
            bullet = Instantiate(bullet, playerBulletPoolObject.transform);
            bullet.transform.position = position;
            playerBulletPool.Add(bullet);
            return bullet;
        }

        // Find if there is an open slot within the pool.
        for (int i = 0; i < playerBulletPool.Count; i++)
        {
            if (playerBulletPool[i] != null)
            {
                if (!playerBulletPool[i].gameObject.activeInHierarchy)
                {
                    playerBulletPool[i].gameObject.SetActive(true);
                    playerBulletPool[i].transform.position = position;
                    return playerBulletPool[i];
                }
            }
            else
            {
                break;
            }
        }

        // If no open spot has been found, spawn an object outside of the pool.
        if (playerBulletPool.Count >= MAX_PLAYER_BULLETS)
        {
            if (playerBulletPool[^1] != null)
            {
                bullet = Instantiate(bullet, playerBulletPoolObject.transform);
                bullet.transform.position = position;
                return bullet;
            }
        }
        // Otherwise, create a new object for the pool.
        bullet = Instantiate(bullet, playerBulletPoolObject.transform);
        bullet.transform.position = position;
        playerBulletPool.Add(bullet);
        return bullet;
    }
    #endregion

    #region Release
    void ReleaseEnemy(Enemy enemy)
    {
        // Find if the enemy is within the pool.
        if (enemyPool.Contains(enemy)) enemy.gameObject.SetActive(false);
        // Otherwise, Destroy the overflow object.
        else Destroy(enemy.gameObject);
    }
    void ReleaseEnemyBullet(EnemyBullet bullet, bool isYellow)
    {
        if (isYellow) ReleaseYellowEnemyBullet(bullet);
        else ReleaseTriangleEnemyBullet(bullet);
    }
    void ReleaseYellowEnemyBullet(EnemyBullet bullet)
    {
        // Find if the enemy is within the pool.
        if (yellowEnemyBulletPool.Contains(bullet)) bullet.gameObject.SetActive(false);
        // Otherwise, Destroy the overflow object.
        else Destroy(bullet.gameObject);
    }
    void ReleaseTriangleEnemyBullet(EnemyBullet bullet)
    {
        // Find if the enemy is within the pool.
        if (triangleEnemyBulletPool.Contains(bullet)) bullet.gameObject.SetActive(false);
        // Otherwise, Destroy the overflow object.
        else Destroy(bullet.gameObject);
    }
    void ReleasePlayerBullet(PlayerBullet bullet)
    {
        // Find if the enemy is within the pool.
        if (playerBulletPool.Contains(bullet)) bullet.gameObject.SetActive(false);
        // Otherwise, Destroy the overflow object.
        else Destroy(bullet.gameObject);
    }
    void ReleaseAll(bool playerIsDead)
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy enemy in enemies)
        {
            if (enemyPool.Contains(enemy))
                enemy.gameObject.SetActive(false);
            else
                Destroy(enemy.gameObject);
        }

        GameObject[] smallSquareParents = GameObject.FindGameObjectsWithTag("SmallSquareParent");
        foreach (GameObject smallSquareParent in smallSquareParents)
        {
            GameObject[] children = smallSquareParent.GetComponentsInChildren<GameObject>();
            foreach (GameObject child in children)
                Destroy(child);
            Destroy(smallSquareParent.gameObject);
        }

        EnemyBullet[] enemyBullets = FindObjectsByType<EnemyBullet>(FindObjectsSortMode.None);
        foreach (EnemyBullet enemyBullet in enemyBullets)
        {
            if (triangleEnemyBulletPool.Contains(enemyBullet))
                enemyBullet.gameObject.SetActive(false);
            else if (yellowEnemyBulletPool.Contains(enemyBullet))
                enemyBullet.gameObject.SetActive(false);
            else
                Destroy(enemyBullet.gameObject);
        }

        PlayerBullet[] playerBullets = FindObjectsByType<PlayerBullet>(FindObjectsSortMode.None);
        foreach (PlayerBullet playerBullet in playerBullets)
        {
            if (playerBulletPool.Contains(playerBullet))
                playerBullet.gameObject.SetActive(false);
            else
                Destroy(playerBullet.gameObject);
        }
    }
    #endregion

}
