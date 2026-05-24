using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSmallSquares : MonoBehaviour
{
    void Start()
    {
        foreach (SquareAI children in GetComponentsInChildren<SquareAI>())
        {
            children.transform.parent = GameObject.Find("Enemies").transform;
        }
        Destroy(gameObject);
    }
}
