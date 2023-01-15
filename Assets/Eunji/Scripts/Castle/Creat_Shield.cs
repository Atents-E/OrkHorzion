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
        // 돈이 있다 
        if (playerGold.nowGold > shield.price )
        {
            // 실드가 있다 없다
            if (isExist)
            {
                // 체력을 확인했는데 풀이 아니다
                if( shield.shildHP != shield.maxHP)
                {
                    // 돈 빼고,
                    playerGold.NowGold -= shield.price;
                    // 충전
                    shield.shildHP = shield.maxHP;
                    // 중첩 표시
                    StartCoroutine(overlap());
                }
                else
                {
                    // 체력이 이미 다 있다는거 알림
                }
            }
            else
            {
                // 돈 빼고,
                playerGold.NowGold -= shield.price;
                // 실드 활성화
                shield.gameObject.SetActive(true);
                // 실드 여부 true로 변경
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