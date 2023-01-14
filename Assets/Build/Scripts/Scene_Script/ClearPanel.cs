using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

// 게임 클리어 패널
// 1. 클리어 시간(패널로 하면서 가능해짐)
// 2. 플레이어가 죽은 횟수
// 3. 몬스터는 생성 횟수
// 4. 돈
public class ClearPanel : MonoBehaviour
{
    /// <summary>
    ///  경과 시간
    /// </summary>
    float time = 0;

    /// <summary>
    /// 플레이어가 최종적으로 들고 있는 돈
    /// </summary>
    int gold = 0;

    /// <summary>
    /// 플레이어가 죽은 횟수
    /// </summary>
    int dieCount = 0;

    /// <summary>
    /// 처치한 몬스터의 수
    /// </summary>
    int monsterKill= 0;

    bool isPlay = false;

    /// <summary>
    /// 게임 재시작 버튼
    /// </summary>
    Button restart;

    TextMeshProUGUI time_Text;
    TextMeshProUGUI gold_Text;
    TextMeshProUGUI die_Text;
    TextMeshProUGUI Kill_Text;

    CanvasGroup canvasGroup;

    Action onOpen;

    private void Awake()
    {
        restart = GetComponentInChildren<Button>();
        restart.onClick.AddListener(Restart);          // 버튼이 눌리면, Start씬으로 이동

        canvasGroup = GetComponent<CanvasGroup>();

        time_Text = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        gold_Text = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        die_Text = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        Kill_Text = transform.GetChild(6).GetComponent<TextMeshProUGUI>();

        Close();
    }

    private void Start()
    {
        WaveSystem StartWave = GameManager.Inst.WaveSystem;
        StartWave.onClear += Open;

        onOpen += TextChange;
    }

    private void Update()
    {
        if (isPlay)
        {
            time += Time.deltaTime;     // 플레이 시간 누적
        }
    }


    void Open()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
        isPlay = false;

        onOpen?.Invoke();
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

    void TextChange()
    {
        PlayerGold playerGold = GameManager.Inst.PlayerGold;

        // 죽은 횟수 찾고
        // 플레이가 죽은 걸 확인하는 순간을 
        
        // 처치한 적 찾아야 함 


        time_Text.text = ($"플레이 시간 : {time}");
        gold_Text.text = ($" 돈 : {playerGold.nowGold}");
        // die_Text.text = ($"죽은 횟수 : {}");
        // Kill_Text.text = ($"처치한 적 : {}");
    }

}