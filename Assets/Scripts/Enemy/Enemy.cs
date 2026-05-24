using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour
{
    //Events
    public static event Action<Transform> _OnRandomItemSpawn;
    public static event Action<GameObject, Transform> _SpawnObject;
    public static event Action<int> _AddToScore;
    public static event Action<Enemy> _Death;

    public EnemyType enemyType;

    //Non-Object Varables
    [SerializeField] private int attack;
    [SerializeField] private int health;
    [SerializeField] private bool isParent;
    [SerializeField] private int score;

    //Object Variables
    [SerializeField] private HealthComponent Health;
    [SerializeField] private PolygonCollider2D coll;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject smallSquare;
    public Waves waves;

    bool isDead = false;

    private void OnEnable()
    {
        isDead = false;
        if (!waves)
            waves = GameObject.FindGameObjectWithTag("Waves").GetComponent<Waves>();
        health = (int)(health * waves.waveMultiplier);
        score = UnityEngine.Random.Range(50, 100);
    }

    public void Hit(int amount)
    {
        if (isDead)
            gameObject.SetActive(false);
        health -= amount;
        if(health <= 0)
        {
            Death();
            isDead = true;
        }
    }

    public void Death()
    {
        if (isDead)
        {
            gameObject.SetActive(false);
            return;
        }

        isDead = true;
        if (isParent && TryGetComponent(out SquareAI _)) _SpawnObject(smallSquare, transform);
        else if (!isParent) Destroy(gameObject);

        float spawnItemChance = UnityEngine.Random.Range(0.0f, 1.0f);

        if (spawnItemChance <= .3f * (1 / waves.waveMultiplier))
        {
            _OnRandomItemSpawn(transform);
        }
        _AddToScore(score);
        _SpawnObject(deathParticles, transform);
        _Death(this);
    }

    private void OnDestroy()
    {
        if (!name.StartsWith("Small"))
            Debug.Log("Destroyed Enemy: " + name);
    }
}

public enum EnemyType
{
    Square,
    Triangle,
    Hexagon
}
