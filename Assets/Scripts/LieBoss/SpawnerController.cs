using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public Transform leftBottomSpawner;
    public Transform leftTopSpawner;
    public Transform rightBottomSpawner;
    public Transform rightTopSpawner;
    public Transform topLeftSpawner;
    public Transform topRightSpawner;

    public GameObject bulletPrefab;
    public float spawnInterval = 1f;

    void Start()
    {
        StartCoroutine(SpawnBullets());
    }

    IEnumerator SpawnBullets()
    {
        while (true)
        {
            SpawnBullet(leftBottomSpawner, Vector2.right);
            SpawnBullet(rightTopSpawner, Vector2.left);
            SpawnBullet(topRightSpawner, Vector2.down);

            yield return new WaitForSeconds(spawnInterval);

            SpawnBullet(rightBottomSpawner, Vector2.left);
            SpawnBullet(leftTopSpawner, Vector2.right);
            SpawnBullet(topLeftSpawner, Vector2.down);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnBullet(Transform spawner, Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawner.position, Quaternion.identity);
        BulletMove bulletMovement = bullet.GetComponent<BulletMove>();
        if (bulletMovement != null)
        {
            bulletMovement.SetDirection(direction); 
        }
    }

}
