using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeathParticles : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2);
    }
}
