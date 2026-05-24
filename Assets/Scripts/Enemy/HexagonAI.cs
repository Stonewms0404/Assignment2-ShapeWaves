using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HexagonAI : MonoBehaviour
{
    public static event Action<GameObject, Transform> _SpawnObject;
    public static event Func<EnemyBullet, Vector2, EnemyBullet> _Shoot;

    private GameObject playerPos;
    private float timer;

    public EnemyBullet enemyBullet;
    public GameObject deathParticles;
    public GameObject[] bulletTransform;
    public Enemy enemy;
    public bool canFire;
    public float timeBetweenFiring;
    public int Speed;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 rotation = playerPos.transform.position - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ + 30);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }
        else
        {
            canFire = false;
            for (int i = 0; i < bulletTransform.Length; i++)
            {
                GameObject bulletTrans = bulletTransform[i];
                EnemyBullet bullet = _Shoot(enemyBullet, bulletTrans.transform.position);
                bullet.originPosition = transform.position;
                if (i == 0) bullet.playAudio = true;
                else bullet.playAudio = false;
            }
        }

        Move();
    }

    private void Move()
    {
        Vector3 direction = playerPos.transform.position - transform.position;
        direction.Normalize();
        direction.z = 0;

        transform.position = Vector3.MoveTowards(transform.position, playerPos.transform.position, Time.deltaTime * Speed);
    }
}
