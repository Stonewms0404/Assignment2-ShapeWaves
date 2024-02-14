using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleAI : MonoBehaviour
{
    private GameObject PlayerPos;
    private float timer;

    public GameObject EnemyBullet;
    public Transform bulletTransform;
    public bool canFire;
    public float timeBetweenFiring;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        PlayerPos = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 rotation = PlayerPos.transform.position - transform.position;

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
            Instantiate(EnemyBullet, bulletTransform.position, transform.rotation);
        }
    }
}
