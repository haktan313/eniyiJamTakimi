using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Combat : MonoBehaviour
{
    [SerializeField] int damageAmountToGiven;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            BP_Health enemyHealth = other.GetComponent<BP_Health>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmountToGiven);
                Debug.Log("hittttt" + other.name);
            }
        }
    }
}
