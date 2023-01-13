using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWave : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textEnemyCount;
    [SerializeField]
    EnemySpawner enemySpawner;

    private void Update()
    {
        textEnemyCount.text = $"waves {enemySpawner.Result} / {enemySpawner.MaxEnemyCount}";
    }

}
