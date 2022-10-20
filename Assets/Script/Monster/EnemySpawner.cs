using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnTime = .0f;

    private void Start()
    {
        
        StartCoroutine(SpawnEnemy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            
            Instantiate(enemyPrefab, transform.position, Quaternion.Euler(0.0f,-90.0f,0.0f));

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
