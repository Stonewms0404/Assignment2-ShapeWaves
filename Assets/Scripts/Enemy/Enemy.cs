using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    //Events
    public static event Action<Transform> _OnRandomItemSpawn;
    public static event Action<GameObject, Transform> _SpawnObject;
    public static event Action<int> _AddToScore;

    //Non-Object Varables
    [SerializeField] private int attack;
    [SerializeField] private int health;
    [SerializeField] private int score;
    [SerializeField] private bool isParent;

    //Object Variables
    [SerializeField] private HealthComponent Health;
    [SerializeField] private PolygonCollider2D coll;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject smallSquare;
    private Waves waves;

    void Start()
    {
        waves = GameObject.FindGameObjectWithTag("Waves").GetComponent<Waves>();
        health = (int)(health * waves.waveMultiplier);
        score = UnityEngine.Random.Range(50, 100);
    }

    public void Hit(int amount)
    {
        health -= amount;
        if(health == 0)
        {
            Death();
        }
    }

    public void Death()
    {
        _SpawnObject(deathParticles, transform);

        float spawnItemChance = UnityEngine.Random.Range(0.0f, 1.0f);

        if (spawnItemChance <= .3f * (1 / waves.waveMultiplier))
        {
            _OnRandomItemSpawn(transform);
        }

        if (isParent)
        {
            _SpawnObject(smallSquare, transform);
        }

        _AddToScore(score);

        Destroy(gameObject);
    }
}