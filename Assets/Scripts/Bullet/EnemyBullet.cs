using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class EnemyBullet : MonoBehaviour
{
    public static event Action<GameObject, Transform> _SpawnObject;
    public static event Action<EnemyBullet, bool> _Death;

    [SerializeField] private GameObject bulletDeathParticles;
    [SerializeField] private AudioSource shotAudio;
    private GameObject playerPos;

    [SerializeField] private float speed;
    [SerializeField] private bool isHoming;
    [SerializeField] private float despawnTimer;
    private float timer, speedTimer, homingDelayTimer;

    public bool isYellow, playAudio = false;
    public Vector2 originPosition;

    Vector2 targetPos;
    float currentSpeed;

    void OnEnable()
    {
        timer = 0;
        speedTimer = 0;
        homingDelayTimer = 0;
        
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        playerPos = GameObject.FindGameObjectWithTag("Player");

        targetPos = playerPos.transform.position;

        if (isHoming)
        {
            if (playAudio) shotAudio.Play();
            else shotAudio.Stop();
            currentSpeed = speed;
        }
        else shotAudio.Play();
    }

    private void FixedUpdate()
    {
        if (isHoming)
        {
            HomeMove();
        }
        else
        {
            MineMove();
        }

        //Replaces Destroy(gameObject, despawnTimer) so the death particles can spawn.
        timer += Time.deltaTime;
        if (timer >= despawnTimer)
        {
            Hit();
        }
    }

    // Moves towards the target position.
    private void MineMove()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
    }
    
    // Follows the player around.
    private void HomeMove()
    {
        speedTimer += Time.deltaTime;
        if ((int)(speedTimer * 100) / 100.0f >= 0.25f)
        {
            speedTimer = 0;
            currentSpeed += 100.0f;
        }

        Vector2 direction = ((Vector2)transform.position - originPosition).normalized;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (homingDelayTimer <= 1f)
        {
            homingDelayTimer += Time.deltaTime;

            targetPos = originPosition + direction * 1200.0f;

            transform.SetPositionAndRotation(
                Vector2.MoveTowards((Vector2)transform.position, targetPos, Time.deltaTime * speed / 2f),
                Quaternion.Euler(Vector3.forward * angle));
            return;
        }
        
        direction = playerPos.transform.position - transform.position;
        direction.Normalize();
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.SetPositionAndRotation(Vector2.MoveTowards(transform.position, playerPos.transform.position, Time.deltaTime * currentSpeed), Quaternion.Euler(Vector3.forward * angle));
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
        _Death(this, isYellow);
    }
}
