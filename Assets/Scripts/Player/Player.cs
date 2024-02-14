using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Events
    public static event Action<GameObject, Transform> _SpawnObject;
    public static event Action<bool> _HitObject;
    public static event Action<bool> _NotDead;
    public static event Action<int> _AddLife;
    public static event Action<int> _PickupItem;
    public static event Action<float> _MachineGunIsActive;
    public static event Action<float> _AddShootSpeed;

    //Non-Object Variables
    [SerializeField] private float Speed;
    [SerializeField] private float timeToSmooth;
    [SerializeField] private float forceDamping;
    private Vector2 MoveInput;
    private bool gameLost = false;
    private Vector2 forceToApply;

    //Object Variables
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private AudioSource pickupAudio;
    [SerializeField] private AudioSource deathAudio;

    //Public Variables
    public bool isDead;
    public DeathMenu.GameType gameType;

    private void OnEnable()
    {
        DeathMenu._GameLost += SetGameLost;
        Timer._TimeOut += TimeOut;
    }
    private void OnDisable()
    {
        DeathMenu._GameLost -= SetGameLost;
        Timer._TimeOut -= TimeOut;
    }

    //Setting the position to be (0, 0, 0).
    private void Start()
    {
        transform.position = Vector3.zero;
    }

    //Obtain the inputs for movement
    void Update()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");

        MoveInput = new Vector2(MoveX, MoveY).normalized;
    }

    private void FixedUpdate()
    {
        if (gameLost)
        {
            isDead = true;
            transform.position = Vector3.zero;
        }

        //If the player is within 100px of (0, 0), the player is no longer dead.
        //The timer is not paused anymore.
        if (!gameLost && Mathf.Abs(Vector3.Distance(transform.position, Vector3.zero)) <= Mathf.Abs(Vector3.Distance(new(100, 0, 0), Vector3.zero)) && isDead)
        {
            isDead = false;
            _NotDead(isDead);
        }

        //The player is able to move if the player is not dead.
        //If the player is dead, it moves the player towards (0, 0).
        //If the game is lost, the player does not move towards (0, 0).
        if (!gameLost && isDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * Speed * 5);
        }
        else if (!gameLost && !isDead)
        {
            Move();
        }
    }

    private void TimeOut(int timeOut)
    {
        gameLost = true;
        Death();
    }

    private void SetGameLost(bool gameIsLost)
    {
        gameLost = true;
        Death();
    }

    //Uses SmoothDamp to move the player across the screen.
    private void Move()
    {
        Vector2 moveForce = Vector2.Lerp(rb.velocity, MoveInput * Speed, forceDamping);
        rb.velocity = moveForce;
    }

    //If an object of a specific type enters the character's collider based upon the object's tag.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            isDead = true;
            Death();
        }
        else if (other.CompareTag("Pickup"))
        {
            Pickup obj = other.GetComponent<Pickup>();
            switch (obj.type)
            {
                case Pickup.PickupType.HEALTH:
                    _AddLife(1);
                    break;
                case Pickup.PickupType.ATTACK:
                    _AddShootSpeed(obj.shootSpeed);
                    break;
                case Pickup.PickupType.MACHINEGUN:
                    _MachineGunIsActive(obj.machineGunTimer);
                    break;
                case Pickup.PickupType.UPTIME:
                    _PickupItem(obj.timeUp);
                    break;
            }
            pickupAudio.Play();
            obj.PickedUp();
        }
    }

    public void Death()
    {
        _HitObject(isDead);
        deathAudio.Play();
        _SpawnObject(deathParticles, transform);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        GameObject[] playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                Destroy(enemies[i]);
            }
        }

        for (int i = 0; i < enemyBullets.Length; i++)
        {
            if (enemyBullets[i] != null)
            {
                Destroy(enemyBullets[i]);
            }
        }

        for (int i = 0; i < playerBullets.Length; i++)
        {
            if (playerBullets[i] != null)
            {
                Destroy(playerBullets[i]);
            }
        }

        for (int i = 0; i < pickups.Length; i++)
        {
            if (pickups[i] != null)
            {
                Destroy(pickups[i]);
            }
        }
    }
}