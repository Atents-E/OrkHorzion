using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

// Castle_HP_Bar의 기능 (수정 필요)
// 1. 거점타워의 체력을 UI로 보여줌

public class Castle_HP_Bar : MonoBehaviour
{
    /// <summary>
    /// 최대체력 숫자
    /// </summary>
    float max_HP;

    /// <summary>
    /// 슬라이더
    /// </summary>
    Slider slider;

    /// <summary>
    /// 체력을 텍스트(숫자)로 보여줌
    /// </summary>
    TextMeshProUGUI HP_Text;

    /// <summary>
    /// 거점타워
    /// </summary>
    Castle castle;

    /// <summary>
    /// 공격 당했음을 표시 할 시간 
    /// </summary>
    WaitForSeconds seeAttack = new WaitForSeconds(0.1f);


    private void Awake()
    {
        HP_Text = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
        slider.value = 1;                               
    }

    private void Start()
    {
        castle = GameManager.Inst.Castle;
        max_HP = castle.maxHP;                          // 최대 HP
        castle.onHealthChange += OnHealthChange;        // 거점타워의 HP가 변경 실행 될 함수 연결
    }

    /// <summary>
    /// HP가 변경되면 변경 될 UI바
    /// </summary>
    /// <param name="ratio"></param>
    private void OnHealthChange(float ratio)
    {
        StartCoroutine(OnHealthChange());       // 체력text의 색상을 변경

        ratio = Mathf.Clamp(ratio, 0, 1);       // 비율의 최소 최대값 고정
        slider.value = ratio;                   // 슬라이더 변경
        float hp = max_HP * ratio;              // 비율을 이용해서 현재 HP 계산
        HP_Text.text = $"{hp:f0} / {max_HP}";      // UI 텍스트 변경(소수점 제거)

        Debug.Log($" 현재 HP : {castle.hp}");
    }

    /// <summary>
    /// 거점 타워가 공격 당하면 체력을 빨간색으로 변경
    /// </summary>
    /// <returns></returns>
    IEnumerator OnHealthChange()
    {
        HP_Text.color = Color.red;    
        yield return seeAttack;         // seeAttack시간이 지나면
        HP_Text.color = Color.black;    // 검정색으로 보임 
    }
}

