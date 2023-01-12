using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;               // 현재 스테이지의 모든 웨이브 정보
    [SerializeField]
    private EnemySpawner enemyspawner;
    private int currentWaveIndex = -1;  // 현재 웨이브 인덱스

    public int CurrentWave => currentWaveIndex + 1; // 시작이 0이기 때문에 +1
    public int MaxWave => waves.Length;

    public void StartWave()
    {
        if (!enemyspawner.IsPlaying  && currentWaveIndex < waves.Length -1)
        {
            // 인덱스의 시작이 -1이기 때문에 웨이브 인덱스 증가를 제일 먼저 함
            currentWaveIndex++;
            // EnemySpawner의 StartWave()함수 호출. 현재 웨이브 정보 제공
            enemyspawner.StartWave(waves[currentWaveIndex]);
        }
    }
}

[System.Serializable]
public struct Wave
{
    public int spawnWave;
    public float spawnTime;             // 현재 웨이브 적 생성 주기
    public int maxEnemyCount;           // 현재 웨이브 적 등장 숫자
    public GameObject[] enemyPrefabs;   // 현재 웨이브 적 등장 종류
}