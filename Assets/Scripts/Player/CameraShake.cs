using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool screenShaking;
    private float shakeMagnitude;

    private void FixedUpdate()
    {
        if (screenShaking)
        {
            ScreenShake(shakeMagnitude);
        }
    }

    private void SetScreenShaking(float shaking)
    {
        screenShaking = true;
        shakeMagnitude = shaking;
    }

    private void ScreenShake(float magnitude)
    {
        transform.position = new(
            transform.position.x * UnityEngine.Random.Range(-magnitude, magnitude),
            transform.position.y * UnityEngine.Random.Range(-magnitude, magnitude),
            0);
        screenShaking = false;
    }
}
