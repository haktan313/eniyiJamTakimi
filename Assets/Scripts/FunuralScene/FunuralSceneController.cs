using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunuralSceneController : MonoBehaviour
{
    public SpriteRenderer imageRenderer;
    public Camera cameraToMatch;

    void Start()
    {
        if (imageRenderer != null && cameraToMatch != null)
        {
            float spriteHeight = imageRenderer.sprite.bounds.size.y;
            float distance = Mathf.Abs(cameraToMatch.transform.position.z - transform.position.z);
            float requiredHeight = 2.0f * distance * Mathf.Tan(cameraToMatch.fieldOfView * 0.5f * Mathf.Deg2Rad);

            float scale = requiredHeight / spriteHeight;
            transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
