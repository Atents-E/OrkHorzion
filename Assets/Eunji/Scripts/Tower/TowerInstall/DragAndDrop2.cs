using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;
using static UnityEditor.Progress;

public class DragAndDrop2 : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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
        currentParent = transform.parent;
        newParent = GameObject.FindWithTag("Canvas");
    }

    /// <summary>
    /// 드래그 시작
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        bool isDragging = false;    
        // 드래그가 시작되면 부모자식 관계 끊기
        transform.parent = null;
        // 새로운 부모관계 맺어주기
        transform.SetParent(newParent.transform);
        // transform.SetParent(worldPositionStays(isDragging));
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
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, LayerMask.GetMask("Ground"))) // 레이와 땅의 충돌 여부 확인
        {
            // 레이와 땅이 충돌했으면
            Debug.Log("레이와 땅이 충돌함");

            Vector3 dropPos = hit.point;    // 피킹한 지점 따로 저장
            
            // 그 지점에 tower 생성
            Instantiate(towerPrefab, dropPos, transform.rotation);
            
        }

        // 부모 끊고, 기존 부모와 다시 맺어주기
        transform.parent = null;
        transform.SetParent(currentParent);
        // 타워 UI = 처음 타워 위치로 이동
        transform.position = currentParent.position;
    }

}
