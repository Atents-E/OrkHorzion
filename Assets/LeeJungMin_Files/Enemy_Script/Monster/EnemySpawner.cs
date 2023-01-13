using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject enemyPrefab;

    List<EnemyBase> enemyList;  // 현재 맵에 존재하는 모든 적의 정보

    public List<EnemyBase> EnemyList => enemyList;

    private Wave currentWave;   // 현재 웨이브 정보

    int waveCount = 0;

    bool isPlaying = false;

    int spawnEnemyCount = 0;

    int result = 0;

    public int Result
    {
        get => result;
        set => result = value;
    }

    public int MaxEnemyCount => currentWave.maxEnemyCount;

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

    private void Awake()
    {
        enemyList = new List<EnemyBase>();
    }

    public void StartWave(Wave wave)
    {
        if (!isPlaying && Result <= 0)
        {
            spawnEnemy = SpawnEnemy();
            // 매개변수로 받아온 웨이브 정보 저장
            currentWave = wave;

            waveCount++;
            // 현재 웨이브 시작
            StartCoroutine(spawnEnemy);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemyBase = other.GetComponent<EnemyBase>();
            DestroyEnemy(enemyBase);
            SetDel();
        }
    }

    IEnumerator SpawnEnemy()
    {
        // 현재 웨이브에서 생성한 적 숫자
        spawnEnemyCount = 0;
        int index = 0;
        isPlaying = true;        
        
        // 현재 웨이브에서 생성되어야 하는 적의 숫자만큼 적을 생성하고 코루틴 종료
        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            switch (currentWave.spawnWave)
            {                
                case 1:
                    Debug.Log("1번 웨이브");
                    index++;
                    result++;
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
                    Debug.Log("2번 웨이브");
                    index++;
                    result++;
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
                    Debug.Log("3번 웨이브");
                    index++;
                    result++;
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
                    Debug.Log("4번 웨이브");
                    index++;
                    result++;
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

            EnemyBase enemy = GetComponent<EnemyBase>();
            enemyList.Add(enemy);

            // 현재 웨이브에서 생성한 적의 숫자 +1
            spawnEnemyCount++;
            
            yield return new WaitForSeconds(currentWave.spawnTime);
        }
        isPlaying = false;        
    }

    /// <summary>
    /// 리스트에 있는 enemy를 지우는 함수
    /// </summary>
    /// <param name="enemy"></param>
    public void DestroyEnemy(EnemyBase enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);

        if (!IsPlaying && Result <= 0)
        {
            GameManager.Inst.RewardPanel.Open(true);
        }
    }

    public void SetDel()
    {
        Result--;
    }
}
