using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Scene_GameOver 기능

// 1. 재시작 버튼을 누르면 메인 씬으로 이동한다.

public class Scene_GameOver : MonoBehaviour
{
    /// <summary>
    /// 게임 재시작 버튼
    /// </summary>
    Button restart;         

    private void Awake()
    {
        restart = GetComponentInChildren<Button>();
        restart.onClick.AddListener(GameOver);          // 버튼이 눌리면, GameOver함수 실행
    }

    /// <summary>
    /// 메인 씬 불러오기   // 추후 (메인 씬)-> 번호로 변경하기
    /// </summary>
    public void GameOver()
    {
        SceneManager.LoadScene("메인 씬");
    }
}
