using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;
using static UnityEditor.Progress;
using Unity.VisualScripting;

public class TowerInstall : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    static int itemCount = 0;   // 생성된 아이템 총 갯수. 아이템 생성 아이디의 역할도 함.
    
    /// <summary>
    /// 기존 부모 오브젝트
    /// </summary>
    Transform currentParent;

    /// <summary>
    /// (드래그 중일 때)변경 될 부모 오브젝트
    /// </summary>
    GameObject newParent;


    /// <summary>
    /// 생성 될 타워 프리팹
    /// </summary>
    public GameObject towerPrefab;

    private void Awake()
    {
        // this.gameObject.SetActive(false);
        currentParent = transform.parent;
        newParent = GameObject.FindWithTag("Canvas");
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
            //Vector2 screenPos = Mouse.current.position.ReadValue(); // 마우스의 위치(스크린좌표)를 구함
            //if (!IsInsideTowerMenu(screenPos))                      // 마우스의 위치가 타워메뉴 밖에서 동작하는지 확인
            //{
            //}

            // 드래그가 시작되면 부모자식 관계 끊기
            transform.parent = null;
            // 새로운 부모관계 맺어주기
            transform.SetParent(newParent.transform);
     
            // bool isDragging = false;    
            // transform.SetParent(worldPositionStays(isDragging));
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


            // 타워를 살 수 있는 돈이 있는지 확인
            // 돈이 있으면 그 지점에 tower 생성
            Instantiate(towerPrefab, creatPos, transform.rotation);
            Debug.Log("타워 설치");
            // 돈이 부족해서 타워를 구매할 수 없다는 창을 띄어줌
        }
        else
        {
        }

        // 부모 끊고, 기존 부모와 다시 맺어주기
        transform.parent = null;
        transform.SetParent(currentParent);
        // 타워 UI = 처음 타워 위치로 이동
        transform.position = currentParent.position;
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
