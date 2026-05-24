using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class TriangleAI : MonoBehaviour
{
    public static event Func<EnemyBullet, Vector2, EnemyBullet> _Shoot;

    [SerializeField] float distanceFromPlayer = 100.0f;
    [SerializeField] float speed = 50.0f;
    [SerializeField] Rigidbody2D rb;

    private GameObject playerPos;
    private float timer;

    public EnemyBullet enemyBullet;
    public Transform bulletTransform;
    public Enemy enemy;
    public bool canFire;
    public float timeBetweenFiring;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotation Control
        Vector2 rotation = playerPos.transform.position - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + 30);

        // Position Control
        Vector2 direction = -rotation.normalized;
        Vector2 targetPosition = (Vector2)playerPos.transform.position + direction * distanceFromPlayer;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        // Shoot Control
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
            _Shoot(enemyBullet, bulletTransform.transform.position);
        }
    }
}
