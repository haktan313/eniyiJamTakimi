using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Karakter Trigger Box'a girdi�inde
        {
            SceneManager.LoadScene(3); // Yeni sahneyi y�kle
        }
    }
}

