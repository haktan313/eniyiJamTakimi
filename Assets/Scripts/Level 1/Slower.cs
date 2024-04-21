using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : MonoBehaviour
{
    [SerializeField]
    private BP_CharacterMovement playerControl;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && playerControl.playerSpeed > 5)
        {
            playerControl.playerSpeed--;
        }
    }
}
