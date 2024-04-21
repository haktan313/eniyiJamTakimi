using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BP_Health : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    public float turnSpeed = 180f; 
    public float fallSpeed = 1f; 
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0) 
        {
            animator.enabled = false;
            Dead();
        }
    }
    void Dead()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        coll.enabled = false;
        StartCoroutine(TurnAndFall());
    }


    IEnumerator TurnAndFall()
    {
        float currentTurnSpeed = 0f;

        while (currentTurnSpeed < turnSpeed)
        {
            float turnAmount = turnSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, turnAmount);
            currentTurnSpeed += turnAmount;
            yield return null;
        }

        while (transform.position.y > -10f) 
        {
            rb.velocity = Vector2.down * fallSpeed;
            yield return null;
        }


        Destroy(gameObject);
    }

}
