using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BP_CharacterMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashValue;

    private int comboStep = 0;
    private float lastAttackTime = 0;
    private float comboMaxDelay = 1.0f;  // Maximum time between combo hits

    private bool isGround = false;
    private bool isDashing = false;
    private Animator playerAnimator;
    private Vector2 playerVector;
    private Rigidbody2D playerRB;
    private CapsuleCollider2D playerCapsuleCollider;
    private CircleCollider2D playerCircleCollider;

    void Start()
    {
        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCircleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (playerVector.x > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (playerVector.x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Reset combo if time exceeded
        if (Time.time - lastAttackTime > comboMaxDelay && comboStep != 0)
        {
            comboStep = 0;
            playerAnimator.ResetTrigger("Attack1");
            playerAnimator.ResetTrigger("Attack2");
            playerAnimator.SetTrigger("ResetCombo");
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            playerRB.velocity = new Vector2(playerVector.x * playerSpeed, playerRB.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            playerAnimator.SetBool("isJumping", false);
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    void OnMove(InputValue playerInputVector)
    {
        playerVector = playerInputVector.Get<Vector2>();
    }

    void OnJump(InputValue playerInputVector)
    {
        if (playerInputVector.isPressed && isGround)
        {
            playerRB.velocity += new Vector2(0, jumpSpeed);
            playerAnimator.SetBool("isJumping", true);
        }
    }

    void OnDash(InputValue playerInputVector)
    {
        if (playerInputVector.isPressed)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        playerAnimator.SetBool("dash", true);
        float originalGravityScale = playerRB.gravityScale;
        playerRB.gravityScale = 0;
        float dashDirection = Mathf.Sign(transform.localScale.x);
        playerRB.velocity = new Vector2(dashDirection * dashValue, 0);

        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
        playerRB.velocity = Vector2.zero;
        playerRB.gravityScale = originalGravityScale;
        playerAnimator.SetBool("dash", false);
    }

    void OnAttack(InputValue playerInputVector)
    {
        if (playerInputVector.isPressed)
        {
            lastAttackTime = Time.time; // Update the last attack time

            if (comboStep == 0)
            {
                playerAnimator.SetTrigger("Attack1");
                comboStep = 1;
            }
            else if (comboStep == 1)
            {
                playerAnimator.SetTrigger("Attack2");
                comboStep = 0;  // Reset combo or set to 2 for a longer combo
            }
        }
    }
}
