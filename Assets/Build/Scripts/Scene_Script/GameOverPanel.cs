using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// GameOverPanel기능

// 1. 거점타워의 체력이 0이 되면 보인다. 
// 2. 재시작 버튼을 누르면 시작 씬으로 이동한다.

public class GameOverPanel : MonoBehaviour
{
    /// <summary>
    /// 게임 재시작 버튼
    /// </summary>
    Button restart;

    Castle castle;

    CanvasGroup canvasGroup;

    private void Awake()
    {
        restart = GetComponentInChildren<Button>();
        restart.onClick.AddListener(Restart);          // 버튼이 눌리면, 게임 시작 씬으로 이동
        canvasGroup = GetComponent<CanvasGroup>();

        Close();
    }

    private void Start()
    {
        castle = GameManager.Inst.Castle;
        castle.isGameOver += Open;
    }

    void Open()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void Close()        
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void Restart()
    {
        SceneManager.LoadScene("Start");
    }

}
