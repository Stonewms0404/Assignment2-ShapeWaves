using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType { HEALTH, ATTACK, MACHINEGUN, UPTIME };
    public float shootSpeed;
    public int timeUp;
    public float machineGunTimer;
    public PickupType type;
    float timer = 0.0f;

    private void Start()
    {
        machineGunTimer = Random.Range(3.0f, 7.0f);
        timeUp = Random.Range(15, 20);
        shootSpeed = Random.Range(0.1f, 0.25f);
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 5.0f)
        {
            PickedUp();
        }
    }

    public void PickedUp()
    {
        Destroy(gameObject);
    }
}
