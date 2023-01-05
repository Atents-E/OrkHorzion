using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject enemyPrefab;

    private Wave currentWave;   // ���� ���̺� ����

    int waveCount = 0;

    bool isPlaying = false;

    public bool IsPlaying
    {
        get => isPlaying;
    }

    IEnumerator spawnEnemy;

    public int WaveCount
    {
        get => waveCount;
        set => waveCount = value;
    }   
    public GameObject EnemyPrefab => enemyPrefab;
    
    public void StartWave(Wave wave)
    {
        spawnEnemy = SpawnEnemy();
        if (!isPlaying)
        {
            // �Ű������� �޾ƿ� ���̺� ���� ����
            currentWave = wave;
            waveCount++;
            // ���� ���̺� ����
            StartCoroutine(spawnEnemy);
        }
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
        // ���� ���̺꿡�� ������ �� ����
        int spawnEnemyCount = 0;
        int index = 0;
        isPlaying = true;        
        // ���� ���̺꿡�� �����Ǿ�� �ϴ� ���� ���ڸ�ŭ ���� �����ϰ� �ڷ�ƾ ����
        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            switch (currentWave.spawnWave)
            {                
                case 1:
                    Debug.Log("1�� ���̺�");
                    index++;                    
                    if (index > 0 && index <= 5)
                    {
                        Instantiate(currentWave.enemyPrefabs[0], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    else if(index > 5 && index <= 8)
                    {
                        Instantiate(currentWave.enemyPrefabs[1], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    else if(index > 8 && index <= currentWave.maxEnemyCount)
                    {
                        Instantiate(currentWave.enemyPrefabs[2], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    break;
                case 2:
                    Debug.Log("2�� ���̺�");
                    index++;
                    if (index > 0 && index <= 3)
                    {
                        Instantiate(currentWave.enemyPrefabs[0], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    else if (index > 3 && index <= 6)
                    {
                        Instantiate(currentWave.enemyPrefabs[1], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    else if (index > 6 && index <= 9)
                    {
                        Instantiate(currentWave.enemyPrefabs[2], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    else if (index > 9 && index <= currentWave.maxEnemyCount)
                    {
                        Instantiate(currentWave.enemyPrefabs[3], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    break;
                case 3:
                    Debug.Log("3�� ���̺�");
                    index++;
                    if (index > 0 && index <= 2)
                    {
                        Instantiate(currentWave.enemyPrefabs[0], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    else if (index > 2 && index <= 4)
                    {
                        Instantiate(currentWave.enemyPrefabs[1], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    else if (index > 4 && index <= 8)
                    {
                        Instantiate(currentWave.enemyPrefabs[2], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    else if (index > 8 && index <= currentWave.maxEnemyCount)
                    {
                        Instantiate(currentWave.enemyPrefabs[3], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    break;
                case 4:
                    Debug.Log("4�� ���̺�");
                    index++;
                    if (index > 0 && index <= 3)
                    {
                        Instantiate(currentWave.enemyPrefabs[0], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    else if (index > 3 && index <= 6)
                    {
                        Instantiate(currentWave.enemyPrefabs[1], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    else if (index > 6 && index <= 9)
                    {
                        Instantiate(currentWave.enemyPrefabs[2], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    else if (index > 9 && index <= currentWave.maxEnemyCount)
                    {
                        Instantiate(currentWave.enemyPrefabs[3], transform.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                    }
                    break;
                default:
                    break;
            }
            

            // ���� ���̺꿡�� ������ ���� ���� +1
            spawnEnemyCount++;
            
            yield return new WaitForSeconds(currentWave.spawnTime);
        }
        isPlaying = false;
    }
}
