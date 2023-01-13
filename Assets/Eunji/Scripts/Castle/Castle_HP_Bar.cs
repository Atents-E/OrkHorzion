using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Castle_HP_Bar의 기능 (수정 필요)
// 1. 거점타워의 체력을 UI로 보여줌

public class Castle_HP_Bar : MonoBehaviour
{
    /// <summary>
    /// 슬라이더
    /// </summary>
    Slider slider;

    /// <summary>
    /// 체력을 텍스트(숫자)로 보여줌
    /// </summary>
    TextMeshProUGUI HP_Text;

    /// <summary>
    /// 최대체력 텍스트
    /// </summary>
    string maxHP_Text;

    /// <summary>
    /// 최대체력 숫자
    /// </summary>
    float max_HP;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        HP_Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        Castle castle = FindObjectOfType<Castle>();     // 추후 게임 매니저로 할당하기
        // Castle castle = GameManager.Inst.Castle;
        //max_HP = castle.maxHP;                          // 최대 HP
        maxHP_Text = $"/{max_HP:f0}";                     // 최대 HP 표시용 글자
        slider.value = 1;                               // 슬라이더 최대치로 해두기
        HP_Text.text = $"{max_HP} / {max_HP}";             // HP는 최대치/최대치
        //castle.onHealthChange += OnHealthChange;        // 거점타워의 HP가 변경되면 실행 되도록 함수 연결
    }

    /// <summary>
    /// HP가 변경되면 표시 될 텍스트(비율)
    /// </summary>
    /// <param name="ratio"></param>
    private void OnHealthChange(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0, 1);       // 비율의 최소 최대값 고정
        slider.value = ratio;               

        float hp = max_HP * ratio;              // 비율을 이용해서 현재 HP 계산

        HP_Text.text = $"{hp:f0}{max_HP}";      // UI 텍스트 변경(소수점 제거)
    }
}
