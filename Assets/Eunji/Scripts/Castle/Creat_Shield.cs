using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Creat_Shield 기능

// 1. 클릭이 되면 거점타워의 자식으로 있던 실드가 활성화 된다
// 2. 실드가 있는데 실드를 한번 더 구매하면, 실드의 HP는 다시 maxHP로 변한다.
public class Creat_Shield : MonoBehaviour
{
    bool isExist = false;

    Castle castle;

    Shield shield;

    Button button;

    /// <summary>
    /// 플레이어가 가진 골드
    /// </summary>
    PlayerGold playerGold;

    /// <summary>
    /// 실드의 중첩 불가 안내
    /// </summary>
    TextMeshProUGUI warningText;

    TextMeshProUGUI priceText;

    WaitForSeconds showTime = new WaitForSeconds(0.7f);

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        warningText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        priceText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        castle = GameManager.Inst.Castle;
        playerGold = GameManager.Inst.PlayerGold;
        shield = castle.transform.GetChild(0).GetComponent<Shield>();

        button.onClick.AddListener(ShieldChange);
    }

    void ShieldChange()
    {
        if (playerGold.nowGold > shield.price)
        {
            playerGold.NowGold -= shield.price;
            Debug.Log($"현재 금액 {playerGold.NowGold}");
            if (isExist)
            {
                shield.shildHP = shield.maxHP;
                StartCoroutine(overlap());
            }
            else
            {
                shield.gameObject.SetActive(true);
                isExist = true;
            }
        }
    }

    /// <summary>
    /// 실드가 중첩되었다고 알림
    /// </summary>
    /// <returns></returns>
    IEnumerator overlap()
    {
        // 중첩되면 표시
        warningText.color = Color.red;
        yield return showTime;

        warningText.color = Color.clear;
        yield return null;
    }
}