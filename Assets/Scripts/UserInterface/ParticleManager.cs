using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject attackPickup;
    public GameObject healthPickup;
    public GameObject scorePickup;
    public GameObject timePickup;

    public DeathMenu.GameType gameType;

    private void OnEnable()
    {
        Enemy._OnRandomItemSpawn += RandomItem;
        Enemy._SpawnObject += InstantiateObject;
        Player._SpawnObject += InstantiateObject;
        PlayerBullet._SpawnObject += InstantiateObject;
        EnemyBullet._SpawnObject += InstantiateObject;
    }
    private void OnDisable()
    {
        Enemy._OnRandomItemSpawn -= RandomItem;
        Enemy._SpawnObject -= InstantiateObject;
        Player._SpawnObject -= InstantiateObject;
        PlayerBullet._SpawnObject -= InstantiateObject;
        EnemyBullet._SpawnObject -= InstantiateObject;
    }

    public void InstantiateObject(GameObject obj, Transform objPosition)
    {
        Instantiate(obj, objPosition.position, Quaternion.identity);
    }

    public void RandomItem(Transform deadEnemy)
    {
        int randNum = UnityEngine.Random.Range(1, 4);

        switch(randNum)
        {
            case 1:
                Instantiate(attackPickup, deadEnemy.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(scorePickup, deadEnemy.position, Quaternion.identity);
                break;
            case 3:
                if (gameType == DeathMenu.GameType.LIVES)
                {
                    Instantiate(healthPickup, deadEnemy.position, Quaternion.identity);
                }
                else if (gameType == DeathMenu.GameType.TIMED)
                {
                    Instantiate(timePickup, deadEnemy.position, Quaternion.identity);
                }
                break;
        }
    }
}
