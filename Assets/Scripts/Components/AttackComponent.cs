using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    private int Attack;

    private void Start()
    {
        Attack = 1;
    }

    public void SetAttack(int amount)
    {
        Attack = amount;
    }

    public int GetAttack()
    {
        return Attack;
    }
}
