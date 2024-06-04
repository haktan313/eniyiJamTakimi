using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;

    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    public void Shake()
    {
        originalPosition = cameraTransform.localPosition;
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShaking", shakeDuration);
    }

    void DoShake()
    {
        float shakeAmountX = Random.Range(-1f, 1f) * shakeMagnitude;
        float shakeAmountY = Random.Range(-1f, 1f) * shakeMagnitude;
        Vector3 newPos = originalPosition + new Vector3(shakeAmountX, shakeAmountY, 0);
        cameraTransform.localPosition = newPos;
    }

    void StopShaking()
    {
        CancelInvoke("DoShake");
        cameraTransform.localPosition = originalPosition;
    }
}
