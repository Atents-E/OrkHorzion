using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Castle의 기능
// 1. 거점타워는 체력이 존재한다.
// 2. 적의 공격에 의해 체력이 줄어든다.
// 3. 체력이 0보다 작거나 같으면 게임이 끝난다.

public class Castle : MonoBehaviour
{
    /// <summary>
    /// 최대 체력(처음 HP)
    /// </summary>
    public float maxHP = 500;

    /// <summary>
    /// 현재 HP
    /// </summary>
    public float hp = 500;

    /// <summary>
    /// 씬을 로드할 숫자
    /// </summary>
    int sceneName = 0;  // --------------------------- 씬 추가하면 할당 할 숫자 변경 예정

    /// <summary>
    /// 게임 오버를 확인하는 델리게이트
    /// </summary>
    Action isGameOver;

    /// <summary>
    /// 체력이 변경 되었는지 확인하는 델리게이트
    /// </summary>
    public Action<float> onHealthChange { get; set; }

    public float HP
    {
        get => hp;              
        set                     
        {
            if(hp != value)
            {
                hp = value;         
                if( hp <= 0)
                {
                    isGameOver?.Invoke();   // 체력이 0이 되었는지 확인
                }

                hp = Mathf.Clamp(0, value, maxHP); 
                
                onHealthChange?.Invoke(hp / maxHP);
            }
        }
    }


    private void Awake()
    {
        hp = maxHP;
        isGameOver += GameOver;             // 체력이 0이 되면 실행 될 함수 연결

    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");     // 게임 오버 씬 로드(씬 추가하면 삭제)
        // SceneManager.LoadScene(sceneName);  // 게임 오버 씬 로드(씬 추가하면 사용)
    }

}
