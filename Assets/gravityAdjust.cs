using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityAdjust : MonoBehaviour
{
    [SerializeField] CinemachineStateDrivenCamera mainCamera;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            KarakterHaraket karakter= other.gameObject.GetComponent<KarakterHaraket>();
            Transform playerTransform = other.gameObject.GetComponent<Transform>();
            Transform cameraTransform = mainCamera.GetComponent<Transform>();
            if (rb != null && playerTransform != null)
            {
                rb.gravityScale = -8f;
                karakter.gravityWay = -1;
                playerTransform.rotation = Quaternion.Euler(0, 0, 180);
                cameraTransform.rotation = Quaternion.Euler(0, 0, 180);

            }
        }
    }
}
