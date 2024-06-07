using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public GameObject spitPrefab; 
    public GameObject soundWavePrefab; 

    public float attackInterval = 2f; 
    public float attackChance = 0.5f;
    public float spitSpeed = 5f;
    public float soundWaveSpeed = 10f;

    [SerializeField]
    private float nextAttackTime = 0f;
    private Animator animator;

    private Transform playerTransform;

    [SerializeField]
    private AudioSource spitSound;

    private BP_Health health;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        health = GetComponent<BP_Health>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();

            nextAttackTime = Time.time + attackInterval;
        }

        if(health.currentHealth <= 0)
        {
            SceneManager.LoadScene(6);
        }
    }

    void Attack()
    {
        ShootSpit();
    }

    void ShootSpit()
    {
        
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float distance = Vector2.Distance(playerTransform.position, transform.position);
        animator.Play("enemyHit");
        GameObject spit = Instantiate(spitPrefab, transform.position, Quaternion.identity);
        

        // Vy = 5x / Vx
        Rigidbody2D rb = spit.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-5, 5*distance / 5);

        spitSound.Play();


    }

}
