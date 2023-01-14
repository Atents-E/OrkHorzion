using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// Creat_Shield 기능
// 1. 클릭이 되면
// 2. 플레이어의 돈을 확인하여
// 3. 돈이 있으면 돈을 실드 가격만큼 빼고,
// 4. 실드 설치(실드는 항상 거점타워의 위치로 설치된다)

public class Creat_Shield : MonoBehaviour
{
    // 거점타워의 자식으로 존재하고, 실드를 충전하면 true가 된다






    ///// <summary>
    ///// 실드의 금액
    ///// </summary>
    //public int price;

    ///// <summary>
    ///// 생성 될 실드 프리팹
    ///// </summary>
    //public GameObject shield;

    ///// <summary>
    ///// 확인 할 위치(거점타워를 중점으로 설치)
    ///// </summary>
    //Castle castle;

    ///// <summary>
    ///// 클릭을 확인 할 버튼
    ///// </summary>
    //Button install;

    ///// <summary>
    ///// 플레이어가 가진 골드
    ///// </summary>
    //PlayerGold playerGold;

    ///// <summary>
    ///// 실드의 중첩 불가 안내
    ///// </summary>
    //TextMeshProUGUI warningText;

    ///// <summary>
    ///// 생성 할 위치
    ///// </summary>
    //Vector3 creatPos;

    //WaitForSeconds showTime = new WaitForSeconds(0.7f);

    //private void Start()
    //{
    //    castle = GameManager.Inst.Castle;
    //    install = GetComponent<Button>();
    //    playerGold = GameManager.Inst.PlayerGold;
    //    warningText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

    //    install.onClick.AddListener(ShidleInstall);
    //}

    ///// <summary>
    ///// 타워를 클릭하면 실행 될 함수
    ///// 돈이 있으면 설치, 돈이 없으면 경고창을 띄운다.
    ///// </summary>
    //void ShidleInstall()
    //{
    //    int nowGold = playerGold.NowGold;

    //    //Shield shield = FindObjectOfType<Shield>();

    //   // if (shield != null)
    //        //현재 씬에 실드가 없다
    //    //{
    //        // 타워를 살 수 있는 돈이 있는지 확인
    //        if (nowGold >= price)
    //        {
    //            // 타워 가격만큼 빼기
    //            playerGold.NowGold -= price;

    //            creatPos = castle.transform.position;
    //            creatPos.y += 3;
    //            Instantiate(shield, creatPos, transform.rotation, transform);      // 실드 생성
    //        }
    //        else
    //        {
    //            StartCoroutine(playerGold.WaringText());                // 돈이 없다고 알려줌
    //        }
    //    //}
    //    //else
    //    //{
    //    //    StartCoroutine(overlap());
    //    //}
    //}

    //    /// <summary>
    //    /// 실드가 중첩되었다고 알림
    //    /// </summary>
    //    /// <returns></returns>
    //    IEnumerator overlap()
    //{
    //    // 중첩되면 표시
    //    warningText.color = Color.red;
    //    yield return showTime;

    //    warningText.color = Color.clear;
    //    yield return null;
    //}

}
