using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BP_Health : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0) 
        {
            Dead();
        }
    }
    void Dead()
    {
       Destroy(gameObject);
    }
}
