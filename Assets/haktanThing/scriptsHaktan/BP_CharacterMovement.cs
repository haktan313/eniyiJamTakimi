using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class BP_CharacterMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashValue;

    bool isGround = false;
    bool isWalking = false;
    bool isDashing = false;
    Animator playerAnimator;
    Vector2 playerVector;
    Rigidbody2D playerRB;
    CapsuleCollider2D playerCapsuleCollider;
    CircleCollider2D playerCircleCollider;

    void Start()
    {
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCircleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if(playerVector.x > 0.01f) 
        { 
            transform.localScale = new Vector3(1, 1, 1); 
        }else if(playerVector.x < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }
    void FixedUpdate()
    {
        if (!isDashing)
        {
            playerRB.velocity = new Vector2(playerVector.x * playerSpeed, playerRB.velocity.y);
        }
        if (Mathf.Abs(playerVector.x) > 0.01f)
        {
            playerAnimator.SetBool("isWalking", true);
            isWalking = true;
        }else
        {
            playerAnimator.SetBool("isWalking", false);
            isWalking = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag ==  "Ground")
        {
            playerAnimator.SetBool("isJumping", false);
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
            playerAnimator.SetBool("isJumping",true);
        }
    }
    void OnDash(InputValue playerInputVector) {
        if(playerInputVector.isPressed == true) {
            Debug.Log("sa");
            StartCoroutine(Dash());
        }
    }
    IEnumerator Dash() {
        isDashing = true;
        float originalGravityScale = playerRB.gravityScale;
        playerRB.gravityScale = 0;
        float dashDirection= Mathf.Sign(transform.localScale.x);
        playerRB.velocity = new Vector2(dashDirection * dashValue, 0);

        yield return new WaitForSeconds(dashCooldown);
        isDashing=false;
        playerRB.velocity = Vector2.zero;
        playerRB.gravityScale = originalGravityScale;
    }
    void OnAttack(InputValue playerInputVector)
    {
        if (playerInputVector.isPressed == true) {
            playerAnimator.SetTrigger("attack");
        }
    }
}
