using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; 
    public float chaseRange = 5f; 
    public float attackRange = 2f;
    public float moveSpeed = 3f; 
    public float attackSpeed = 1f;
    private bool isAttacking = false; 

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            Attack();
        }
        else if (distanceToPlayer <= chaseRange && !isAttacking)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        Vector2 targetDirection = (player.position - transform.position).normalized;

        Vector2 newEnemyPosition = (Vector2)transform.position + targetDirection * moveSpeed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, moveSpeed * Time.deltaTime);
        if (hit.collider != null && hit.collider.tag != "Player")
        {
            Vector2 hitNormal = hit.normal;
            targetDirection = Vector2.Reflect(targetDirection, hitNormal);
            newEnemyPosition = (Vector2)transform.position + targetDirection * moveSpeed * Time.deltaTime;
        }

        transform.position = newEnemyPosition;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void Attack()
    {
        isAttacking = true;

        Debug.Log("Saldýrýyor");
        Invoke("FinishAttack", attackSpeed);
    }

    void FinishAttack()
    {
        isAttacking = false;
    }
}
