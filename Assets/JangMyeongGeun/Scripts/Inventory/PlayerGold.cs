using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// PlayerGold의 기능
// 1. 몬스터가 죽으면 드랍되는 모든 골드의 금액을 저장
// 2. 골드 금액이 변경될 때마다 UI의 골드 금액도 변경되어 표시  

public class PlayerGold : MonoBehaviour
{
    int maxGold = 1000000000;     // 최대 금액
    public int nowGold = 100;            // 현재 금액

    TextMeshProUGUI gold_Text;           // 텍스트로 표시 할 골드 금액
    Action OnGoldChange;                 // 델리게이트로 골드 소유량 변경 확인

    public int NowGold
    {
        get => nowGold;
        set
        {
            if (nowGold != value)
            {
                nowGold = value;
                nowGold = Mathf.Clamp(value, 0, maxGold);

                OnGoldChange?.Invoke();
            }
        }
    }

    private void Awake()
    {
        gold_Text = transform.GetComponentInChildren<TextMeshProUGUI>();
        gold_Text.text = $"{nowGold}";             // 골드 금액 표시

        OnGoldChange += GoldChange;
    }

    /// <summary>
    /// 변경 된 금액으로 UI 숫자 변경
    /// </summary>
    private void GoldChange()
    {
        gold_Text.text = $"{nowGold}";             // 골드 금액 표시
        Debug.Log($"현재 소유 골드 : {nowGold}");
    }
}
