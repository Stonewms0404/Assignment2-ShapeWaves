using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private int Health;

    private void Start()
    {
        Health = 1;
    }

    public void SetHealth(int amount)
    {
        Health = amount;
    }

    public int GetHealth()
    {
        return Health;
    }

    public void GotHit(int amount)
    {
        Health -= amount;
    }
}
