using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;               // ���� ���������� ��� ���̺� ����
    [SerializeField]
    private EnemySpawner enemyspawner;
    private int currentWaveIndex = -1;  // ���� ���̺� �ε���

    public void StartWave()
    {
        if (!enemyspawner.IsPlaying  && currentWaveIndex < waves.Length -1)
        {
            // �ε����� ������ -1�̱� ������ ���̺� �ε��� ������ ���� ���� ��
            currentWaveIndex++;
            // EnemySpawner�� StartWave()�Լ� ȣ��. ���� ���̺� ���� ����
            enemyspawner.StartWave(waves[currentWaveIndex]);
        }
    }
}

[System.Serializable]
public struct Wave
{
    public int spawnWave;
    public float spawnTime;             // ���� ���̺� �� ���� �ֱ�
    public int maxEnemyCount;           // ���� ���̺� �� ���� ����
    public GameObject[] enemyPrefabs;   // ���� ���̺� �� ���� ����
}