using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements.Experimental;

public class PlayerBullet : MonoBehaviour
{
    public static event Action<GameObject, Transform> _SpawnObject;

    private Vector3 mousePos;
    private Camera mainCam;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject bulletDeathParticles;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private AudioSource shotAudio;

    [SerializeField] private float speed;

    void Start()
    {
        shotAudio.Play();
        int randNum = UnityEngine.Random.Range(-150, 150);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new(mousePos.x + randNum, mousePos.y + randNum, 0);
        Vector3 direction = mousePos - transform.position;
        direction.z = 0;
        rb.velocity = UnityEngine.Random.Range(0.75f, 1.25f) * speed * (Vector2)direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy obj = other.GetComponent<Enemy>();
            obj.Hit(1);
            Hit();
        }
        if (other.CompareTag("EnemyBullet"))
        {
            EnemyBullet obj = other.GetComponent<EnemyBullet>();
            obj.Hit();
            Hit();
        }
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
