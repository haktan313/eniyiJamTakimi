using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KarakterHaraket : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpSpeed;
    public float wallJumpSpeed;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashValue;
    [SerializeField] bool isAttacking = false;
    public int gravityWay = 1;
    bool isGround = false;
    bool isWalking = false;
    bool isDashing = false;
    bool isTutunma = false;
    int comboInt = 0;

    Animator playerAnimator;
    Vector2 playerVector;
    Rigidbody2D playerRB;
    CapsuleCollider2D playerCapsuleCollider;
    CircleCollider2D playerCircleCollider;

    [SerializeField]
    private AudioSource dashSound;
    [SerializeField]
    private AudioSource punchSound;
    [SerializeField]
    private AudioSource walkSound;

    private bool isInTrigger = false;
    [SerializeField]
    private Elevator currentElevator;
    [SerializeField]
    private GameObject kapi;

    [SerializeField]
    TextMeshProUGUI memoryText;

    int aniCount = 0;

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

        if (isInTrigger && Input.GetKeyDown(KeyCode.E) && currentElevator != null)
        {
            Debug.Log("E tu�una bas�ld� ve asans�r tetiklendi.");
            currentElevator.MoveToTarget();
        }
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            if (!isDashing)
            {
                playerRB.velocity = new Vector2(playerVector.x * playerSpeed * gravityWay, playerRB.velocity.y);
            }
            if (Mathf.Abs(playerVector.x) > 0.01f)
            {
                playerAnimator.SetBool("isWalking", true);
                isWalking = true;
            }
            else
            {
                playerAnimator.SetBool("isWalking", false);
                isWalking = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            playerAnimator.SetBool("isJumping", false);
            isGround = true;
            isTutunma = false;
        }
        if (other.gameObject.tag == "tutunmaWall")
        {
            playerRB.gravityScale = 0;
            isTutunma = true;
            playerAnimator.SetBool("isJumping", false);
            playerAnimator.SetBool("tutunma", true);
            Debug.Log("tutundu");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
        if (other.gameObject.tag == "tutunmaWall")
        {
            isTutunma = false;
            playerRB.gravityScale = 8;
            playerAnimator.SetBool("tutunma", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ElevatorButton")
        {
            Debug.Log("Elevator Button is in trigger");
            isInTrigger = true;
        }


        if (other.gameObject.tag == "memory1")
        {
            Debug.Log("Memory1");
            memoryText.enabled = true;
            memoryText.text = "Aaaa Evet K�rm�z� Tuborg Severdi";
            aniCount++;
        }
        if (other.gameObject.tag == "memory2")
        {
            Debug.Log("Memory2");
            memoryText.enabled = true;
            memoryText.text = "Saclari sapsariydi";
            aniCount++;
        }
        if (other.gameObject.tag == "memory3")
        {
            Debug.Log("Memory3");
            memoryText.enabled = true;
            memoryText.text = "Her zaman o pandora kolyeyi takard�";
            aniCount++;
        }
        if (other.gameObject.tag == "memory4")
        {
            Debug.Log("Memory4");
            memoryText.enabled = true;
            memoryText.text = "Arabesk dinlemi cok severdi ozellikle Ferdi Tayfur";
            aniCount++;
        }
        if (other.gameObject.tag == "memory5")
        {
            Debug.Log("Memory5");
            memoryText.enabled = true;
            memoryText.text = "Rotweiller bir kopegi vardi 12 yasinda olmustu ona yenisini almistim";
            aniCount++;
        }
        
        if (aniCount == 5)
        {
            Destroy(kapi);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "ElevatorButton")
        {
            isInTrigger = false;
        }


        if (other.gameObject.tag == "memory1")
        {
            Debug.Log("Memory1");
            memoryText.enabled = false;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "memory2")
        {
            Debug.Log("Memory2");
            memoryText.enabled = false;
            Destroy(other.gameObject);

        }
        if (other.gameObject.tag == "memory3")
        {
            Debug.Log("Memory3");
            memoryText.enabled = false;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "memory4")
        {
            Debug.Log("Memory4");
            memoryText.enabled = false;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "memory5")
        {
            Debug.Log("Memory5");
            memoryText.enabled = false;
            Destroy(other.gameObject);
        }


    }

    void OnMove(InputValue playerInputVector)
    {
        playerVector = playerInputVector.Get<Vector2>();
        Debug.Log("Moving: " + playerVector);
        walkSound.Play();
    }

    void OnJump(InputValue playerInputVector)
    {
        if (playerInputVector.isPressed == true && isGround)
        {
            playerRB.velocity += new Vector2(0, jumpSpeed * gravityWay);
            playerAnimator.SetBool("isJumping", true);
        }
        if (playerInputVector.isPressed == true && isTutunma)
        {
            Debug.Log("JumpingfromWall");
            playerRB.gravityScale = 8;
            playerRB.velocity += new Vector2(0, wallJumpSpeed * gravityWay);
            playerAnimator.SetBool("isJumping", true);
        }
    }

    void OnDash(InputValue playerInputVector)
    {
        if (playerInputVector.isPressed == true)
        {
            Debug.Log("sa");
            StartCoroutine(Dash());
            dashSound.Play();
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        playerAnimator.Play("Dash");
        float originalGravityScale = playerRB.gravityScale;
        playerRB.gravityScale = 0;
        float dashDirection = Mathf.Sign(transform.localScale.x);
        playerRB.velocity = new Vector2(dashDirection * dashValue * gravityWay, 0);

        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
        playerRB.velocity = Vector2.zero;
        playerRB.gravityScale = originalGravityScale;
    }

    void OnAttack(InputValue playerInputVector)
    {
        if (playerInputVector.isPressed == true && !isAttacking)
        {
            if (comboInt == 0)
            {
                playerAnimator.Play("hit1");
                comboInt = 1;
            }
            else if (comboInt == 1)
            {
                playerAnimator.Play("Hit2");
                comboInt = 0;
            }
            punchSound.Play();
        }
    }

    void OnTriggerElevator(InputValue playerInputVector)
    {
        if (playerInputVector.isPressed == true && currentElevator != null && isInTrigger)
        {
            currentElevator.MoveToTarget();
        }
        Debug.Log("Elevator");
    }
}
