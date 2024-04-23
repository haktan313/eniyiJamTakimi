using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{



    public Transform player;
    public float chaseRange = 5f;
    public float attackRange = 5f;
    public float moveSpeed = 3f;
    public float attackSpeed = 3f;
    public bool isAttacking = false;
    public bool isGround = false;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (isGround)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            Debug.Log("DÝSTANCE : " + distanceToPlayer);

            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                Attack();
            }
            else if (distanceToPlayer <= chaseRange && !isAttacking)
            {
                ChasePlayer();
            }
            else if(distanceToPlayer >= chaseRange)
            {
                rb.velocity = Vector2.zero;
            }
            animator.SetFloat("ratSpeed", Mathf.Abs(rb.velocity.x));
        }
    }

    void ChasePlayer()
    {
        Vector2 targetDirection = (player.position - transform.position).normalized;
        rb.velocity = targetDirection * moveSpeed;

        if (rb.velocity.x > 0)
        { rbSprite.flipX = true; }
        else { rbSprite.flipY = false; }
    }

    void Attack()
    {
        rb.velocity = Vector2.zero;
        isAttacking = true;
        player.GetComponent<BP_Health>().currentHealth -= 5;
        Debug.Log("Düþman Saldýrýyor!");
        Invoke("FinishAttack", attackSpeed);
        animator.SetBool("isAttacking", isAttacking);
    }

    void FinishAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }













    //public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    //public UnityEvent OnAttack;

    //[SerializeField]
    //private Transform player;

    //[SerializeField]
    //private float chaseDistanceThreshold = 3, attackDistanceThreshold = 0.8f;

    //[SerializeField]
    //private float attackDelay = 1;
    //private float passedTime = 0;

    //private void Update()
    //{
    //    if(player == null) {  return; }

    //    float distance = Vector2.Distance(player.position, transform.position);

    //    if(distance < chaseDistanceThreshold) { 
    //        OnPointerInput?.Invoke(player.position);
    //        if(distance <= attackDistanceThreshold )
    //        {
    //            OnMovementInput?.Invoke(Vector2.zero);
    //            if(passedTime >= attackDelay)
    //            {
    //                passedTime = 0;
    //                OnAttack?.Invoke();
    //            }
    //        }
    //        else
    //        {
    //            Vector2 direction = player.position - transform.position;
    //            OnMovementInput?.Invoke(direction.normalized);
    //        }
    //    }
    //    if(passedTime < attackDelay)
    //    {
    //        passedTime += Time.deltaTime;
    //    }
    //}



}
