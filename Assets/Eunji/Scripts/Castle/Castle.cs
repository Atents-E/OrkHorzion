using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    EnemyBase enemyBase;

    const float maxHP = 500;
    public float hp = maxHP;

    //Action isGameOver;
    public float HP
    {
        get => hp;              
        set                     
        {
            hp -= value;
            if (hp < 0)
            {
                // 게임 종료 씬 호출;
                GameOver();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("트리거");
            enemyBase = other.GetComponent<EnemyBase>();
            HP = enemyBase.attackPower;
            enemyBase.Die();
        }
    }

    void GameOver()
    {
        
    }
}
