using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject spitPrefab; 
    public GameObject soundWavePrefab; 

    public float attackInterval = 2f; 
    public float attackChance = 0.5f;
    public float spitSpeed = 5f;

    [SerializeField]
    private float nextAttackTime = 0f;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();

            nextAttackTime = Time.time + attackInterval;
        }
    }

    void Attack()
    {
        float randomValue = Random.value;

        if (randomValue < attackChance)
        {
            ShootSpit();
        }
        else
        {
            ShootSoundWave();
        }
    }

    void ShootSpit()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float distance = Vector2.Distance(playerTransform.position, transform.position);

        GameObject spit = Instantiate(spitPrefab, transform.position, Quaternion.identity);
        

        // Vy = 5x / Vx


        Rigidbody2D rb = spit.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-5, 5*distance / 5);
    }

    void ShootSoundWave()
    {
        Instantiate(soundWavePrefab, transform.position, Quaternion.identity);
    }
}
