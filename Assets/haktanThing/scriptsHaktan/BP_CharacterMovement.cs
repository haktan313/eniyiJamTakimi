using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BP_CharacterMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpSpeed;

    bool isGround = false;
    Animator playerAnimator;
    Vector2 playerVector;
    Rigidbody2D playerRB;
    CapsuleCollider2D playerCapsuleCollider;

    void Start()
    {
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        playerRB.velocity = new Vector2(playerVector.x * playerSpeed, playerRB.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag ==  "Ground")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground") 
        {
            isGround = false; 
        }
    }

    void OnMove(InputValue playerInputVector) {
        playerVector = playerInputVector.Get<Vector2>();
        Debug.Log("Moving: " + playerVector);
    }
    void OnJump(InputValue playerInputVector) {
        if(playerInputVector.isPressed == true && isGround) {
            playerRB.velocity += new Vector2(0, jumpSpeed);
        }
    }
}
