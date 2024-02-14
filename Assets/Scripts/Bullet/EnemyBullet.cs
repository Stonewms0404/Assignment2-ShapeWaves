using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class EnemyBullet : MonoBehaviour
{
    public static event Action<GameObject, Transform> _SpawnObject;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject bulletDeathParticles;
    [SerializeField] private AudioSource shotAudio;
    private GameObject playerPos;

    [SerializeField] private float speed;
    [SerializeField] private bool isHoming;
    [SerializeField] private float despawnTimer;
    private float timer;

    void Start()
    {
        shotAudio.Play();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        playerPos = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = playerPos.transform.position - transform.position;
        direction.z = 0;
        rb.velocity = (Vector2)direction.normalized * speed;
    }

    private void FixedUpdate()
    {
        if (isHoming)
        {
            Move();
        }

        //Replaces Destroy(gameObject, despawnTimer) so the death particles can spawn.
        timer += Time.deltaTime;
        if (timer >= despawnTimer)
        {
            Hit();
        }
    }

    //Follows the player around.
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPos.transform.position, Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Walls"))
        {
            Hit();
        }
    }

    public void Hit()
    {
        _SpawnObject(bulletDeathParticles, transform);
        Destroy(gameObject);
    }
}