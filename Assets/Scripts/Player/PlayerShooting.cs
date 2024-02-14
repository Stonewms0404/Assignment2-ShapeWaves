using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Vector3 shootAtPos;
    private float timerFastShot;
    private float timerBurstShot;
    private float timerMachineGun;
    private float timeBetweenFastFiring;
    private float timeBetweenBurstFiring;
    private float timeLeftOnMachineGun;
    private float attackSpeedModifier = 1.0f;
    private bool isPlayerDead = false;
    private bool machineGunActive = false;
    private bool canFireFast = false;
    private bool canFireBurst = false;

    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform shootAtRotator;

    [SerializeField] private float shootFastSpeed;
    [SerializeField] private float shootBurstSpeed;
    [SerializeField] private float maxAngleVariation;


    private void OnEnable()
    {
        Player._HitObject += SetIsPlayerDead;
        Player._NotDead += SetIsPlayerDead;
        Player._AddShootSpeed += AddShootSpeed;
        Player._MachineGunIsActive += SetMachineGunActive;
    }
    private void OnDisable()
    {
        Player._HitObject -= SetIsPlayerDead;
        Player._NotDead -= SetIsPlayerDead;
        Player._AddShootSpeed -= AddShootSpeed;
        Player._MachineGunIsActive -= SetMachineGunActive;
    }

    private void Start()
    {
        SetShooting();
    }

    void FixedUpdate()
    {
        if (!isPlayerDead)
        {
            Shoot();
        }

        if (machineGunActive)
        {
            if (timeLeftOnMachineGun >= 0)
            {
                timeLeftOnMachineGun -= Time.deltaTime;
                machineGunActive = true;
            }
            else
            {
                SetShooting();
                machineGunActive = false;
            }
        }
    }

    private void SetMachineGunActive(float gunTimer)
    {
        if (machineGunActive)
        {
            timeLeftOnMachineGun += gunTimer;
        }
        else
        {
            timeLeftOnMachineGun += gunTimer;
            timeBetweenFastFiring = 40;
            timeBetweenBurstFiring = 8;
            machineGunActive = true;
        }
    }

    private void SetIsPlayerDead(bool playerDead)
    {
        isPlayerDead = playerDead;
        if (playerDead)
            SetShooting();
    }

    private void AddShootSpeed(float value)
    {
        attackSpeedModifier = Math.Clamp(attackSpeedModifier + value, 1, 4.0f);
    }
    private void SetShooting()
    {
        timeBetweenFastFiring = shootFastSpeed;
        timeBetweenBurstFiring = shootBurstSpeed;
    }

    private void Shoot()
    {
        Quaternion shootAt = shootAtRotator.transform.rotation;
        transform.rotation = shootAt;

        shootAtPos = new(shootAtPos.x, shootAtPos.y, shootAtPos.z);

        Vector3 rotation = shootAtPos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        if (!canFireFast)
        {
            timerFastShot += Time.deltaTime;
            if (timerFastShot > 1 / (attackSpeedModifier * timeBetweenFastFiring))
            {
                canFireFast = true;
                timerFastShot = 0;
            }
        }
        if (Input.GetAxis("Fire1") != 0 && canFireFast && canFireBurst)
        {
            canFireFast = false;
            int randNum = UnityEngine.Random.Range(1, 3);
            transform.rotation = Quaternion.Euler(0, 0, rotZ);

            for (int i = 0; i < randNum; i++)
            {
                Instantiate(Bullet, transform.position, Quaternion.identity);
            }
        }

        if (!canFireBurst)
        {
            timerBurstShot += Time.deltaTime;
            if (timerBurstShot > 1 / (attackSpeedModifier * timeBetweenBurstFiring))
            {
                canFireBurst = true;
                timerBurstShot = 0;
            }
        }
        if (Input.GetAxis("Fire2") != 0 && canFireFast && canFireBurst)
        {
            canFireBurst = false;
            int randNum = UnityEngine.Random.Range(10, 15);
            transform.rotation = Quaternion.Euler(0, 0, rotZ * 2);

            for (int i = 0; i < randNum; i++)
            {
                Instantiate(Bullet, transform.position, Quaternion.identity);
            }
        }
    }
}
