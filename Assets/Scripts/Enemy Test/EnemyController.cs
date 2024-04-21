using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; 
    public float chaseRange = 5f; 
    public float attackRange = 5f; 
    public float moveSpeed = 3f; 
    public float attackSpeed = 3f; 
    private bool isAttacking = false; 

    private Animator animator;
    private Rigidbody2D rb;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {      
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Debug.Log("DÝSTANCE : " + distanceToPlayer);

        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            Attack();
        }
        else if (distanceToPlayer <= chaseRange && !isAttacking)
        {
            animator.SetFloat("ratSpeed", Mathf.Abs(rb.velocity.x));
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        Vector2 targetDirection = (player.position - transform.position).normalized;
        rb.velocity = targetDirection * moveSpeed;

        //float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

    }

    void Attack()
    {
        animator.SetBool("isAttacking", isAttacking);
        isAttacking = true;
        Debug.Log("Düþman Saldýrýyor!");
        Invoke("FinishAttack", attackSpeed);
    }

    void FinishAttack()
    {
        animator.SetBool("isAttacking", isAttacking);
        isAttacking = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
