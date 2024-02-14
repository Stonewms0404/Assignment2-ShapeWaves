using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HexagonAI : MonoBehaviour
{
    private GameObject playerPos;
    private float timer;

    public GameObject EnemyBullet;
    public GameObject deathParticles;
    public GameObject BulletTransform;
    public GameObject Hexagon;
    public Enemy enemy;
    public bool canFire;
    public bool ableToFire;
    public float timeBetweenFiring;
    public float maxAngleVariation;
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

            Instantiate(EnemyBullet, BulletTransform.transform.position, transform.rotation);
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

    public void Death()
    {
        Instantiate(deathParticles, transform);
        Destroy(gameObject);
    }
}
