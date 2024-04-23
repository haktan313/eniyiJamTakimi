using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<BP_Health>().currentHealth -= 15;
        }
        
        if(collision.gameObject.tag != "Boss")
        {
            Destroy(gameObject);
        }
    }
}
