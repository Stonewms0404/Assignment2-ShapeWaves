using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticles : MonoBehaviour
{
    public void Despawn()
    {
        Destroy(gameObject, 5);
        ParticleSystem particles = GetComponent<ParticleSystem>();
        particles.Stop();
    }
}
