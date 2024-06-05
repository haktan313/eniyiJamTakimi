using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Vector3 pointA; 
    public Vector3 pointB;
    public float speed = 2.0f;

    private Vector3 targetPoint; 
    private bool isMoving = false; 

    void Start()
    {
        
        targetPoint = pointA;
        transform.position = pointA;
    }

    public void MoveToTarget()
    {
        Debug.Log("Asansör hareket ettirme isteði geldi.");
        if (!isMoving)
        {
            Debug.Log("Asansör hareket ediyor...");
            
            targetPoint = targetPoint == pointA ? pointB : pointA;
            StartCoroutine(MoveElevator(targetPoint));
        }
    }

    IEnumerator MoveElevator(Vector3 target)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}
