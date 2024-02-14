using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    [SerializeField] private GameObject circleMovement;
    [SerializeField] private float speed;

    private Vector3 moveTo = Vector3.zero;
    private Vector3 vel;

    void Start()
    {
        circleMovement.transform.position = new(1, 0, 0);
    }

    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, moveTo)) <= 0.1f)
        {
            moveTo = new(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), 0);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, moveTo, ref vel, speed);
        }
    }
}
