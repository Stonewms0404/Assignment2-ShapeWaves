using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new(UnityEngine.Random.Range(-500, 500), UnityEngine.Random.Range(-500, 500), 0);
        transform.localScale = new(UnityEngine.Random.Range(1, 15), UnityEngine.Random.Range(1, 15), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
