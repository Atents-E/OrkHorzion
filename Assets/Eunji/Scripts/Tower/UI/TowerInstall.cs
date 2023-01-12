using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TowerInstall : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// 생성 될 타워 프리팹
    /// </summary>
    public GameObject towerPrefab;

    /// <summary>
    /// 기존 부모 오브젝트
    /// </summary>
    Transform currentParent;

    /// <summary>
    /// (드래그 중일 때)변경 될 부모 오브젝트
    /// </summary>
    Canvas newParent;

    /// <summary>
    /// 플레이어가 가진 골드
    /// </summary>
    PlayerGold playerGold;

    /// <summary>
    /// 타워 금액 비교용
    /// </summary>
    TowerBase towerGold;

    /// <summary>
    /// 타워의 금액 표시용
    /// </summary>
    TextMeshProUGUI price_Text;

    /// <summary>
    /// 골드 부족 안내
    /// </summary>
    TextMeshProUGUI warningText;

    /// <summary>
    /// 골드 부족 안내 지속되는 시간
    /// </summary>
    WaitForSeconds showTime = new WaitForSeconds(0.7f);


    private void Awake()
    {
        currentParent = transform.parent;

        // 타워에 가격을 확인하여 타워 금액으로 설정
        price_Text = GetComponentInParent<TowerInstall_Icon>().transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        // price_Text = GetComponentInParent<TowerInstall_Icon>().transform.GetComponentInChildren<TextMeshProUGUI>();

        towerGold = towerPrefab.GetComponent<TowerBase>();
        price_Text.text = ($"{towerGold.price}");
    }

    private void Start()
    {
        playerGold = GameManager.Inst.PlayerGold;      
        newParent = GameManager.Inst.Canvas; //GameObject.FindWithTag("Canvas");
        warningText = newParent.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        warningText.color = Color.clear;    // 안내창 알파값1로 초기화

        //isLack += WarningText;              // 돈이 부족하면 안내창 알파값 0으로 변경
    }

    /// <summary>
    /// 드래그 시작
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 열려있는 상태일 때만 처리
        if (gameObject.activeSelf)
        {
            // 새로운 부모관계 맺어주기
            transform.SetParent(newParent.transform, worldPositionStays : false);
        }
    }

    /// <summary>
    /// 드래그 중
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        // 타워 UI = 마우스 위치를 받아서 이동
        transform.position = eventData.position;
    }

    /// <summary>
    /// 드래그 끝
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        int nowGold = playerGold.NowGold;
        int price = towerGold.price;

        // 타워를 살 수 있는 돈이 있는지 확인
        if (nowGold >= price)
        {
            // 타워 가격만큼 빼기
            playerGold.NowGold -= price;
            Debug.Log("타워를 구매 했습니다");

            // 드롭 지점 지점을 체크
            Ray ray =  Camera.main.ScreenPointToRay(eventData.position);   // 스크린 좌표로 레이 생성
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, LayerMask.GetMask("Ground"))) // 레이와 땅의 충돌 여부 확인
            {
                // 레이와 땅이 충돌했으면
                //Debug.Log("레이와 땅이 충돌함");
            
                GameObject ground = hit.collider.gameObject;        // 충돌한 지점의 오브젝트를 찾아서
                Vector3 creatPos = ground.transform.position;       // 찾은 오브젝트의 피벗을 중점으로 위치값 변경(오브젝트의 중간에 오도록)
                creatPos.x += 1;
                creatPos.y = 0;
                creatPos.z -= 1;

                Instantiate(towerPrefab, creatPos, transform.rotation);
                Debug.Log("타워 설치");
            }
        }
        else
        {
            // 골드가 부족하다고 안내
            StartCoroutine(WaringText());
        }

        // 부모 끊고, 기존 부모와 다시 맺어주기
        transform.SetParent(currentParent, worldPositionStays: false);

        // 타워 UI = 처음 타워 위치로 이동
        transform.position = currentParent.position;
    }

    /// <summary>
    /// showTime만큼 안내창을 보여주고, 투명하게 만듬
    /// </summary>
    /// <returns></returns>
    IEnumerator WaringText()
    {
        warningText.color = Color.red;
        yield return showTime;

        warningText.color = Color.clear;
        yield return null;
    }

    ///<summary>
    ///TowerMenu의 크기
    ///</summary>
    ///<param name = "screenPos" ></ param >
    ///< returns ></ returns >
    bool IsInsideTowerMenu(Vector2 screenPos)
    {
        RectTransform rectTransform = (RectTransform)transform;

        Vector2 min = new(rectTransform.position.x - rectTransform.sizeDelta.x, rectTransform.position.y - rectTransform.position.y);
        Vector2 max = new(rectTransform.position.x + rectTransform.sizeDelta.x, rectTransform.position.y + rectTransform.position.y);

        return (min.x < screenPos.x && screenPos.x < max.x && min.y < screenPos.y && screenPos.y < max.y);
    }
}
