using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragAndDrop2 : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    /// <summary>
    /// 기존 부모 오브젝트
    /// </summary>
    Transform parentPosition;

    /// <summary>
    /// (드래그 중일 때)변경 될 부모 오브젝트
    /// </summary>
    GameObject canvas;

    private void Awake()
    {
        parentPosition = transform.parent;
        canvas = GameObject.FindWithTag("Canvas");
    }

    /// <summary>
    /// 드래그 시작
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그가 시작되면 부모자식 관계 끊기
        transform.parent = null;
        // 새로운 부모관계 맺어주기
        transform.SetParent(canvas.transform);
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
        // 부모 끊고, 기존 부모와 다시 맺어주기
        transform.parent = null;
        transform.SetParent(parentPosition);
        // 타워 UI = 처음 타워 위치로 이동
        transform.position = parentPosition.position;


        // towerDataManager.cs에서 생성 할 타워의 정보를 받아오고
        // tower 생성
        // Instantiate(
    }

    static int itemCount = 0;   // 생성된 아이템 총 갯수. 아이템 생성 아이디의 역할도 함.

    /// <summary>
    /// ItemIDCode로 아이템 생성
    /// </summary>
    /// <param name="code">생성할 아이템 코드</param>
    /// <returns>생성 결과</returns>
    public static GameObject TowerOjbectInstall(TowerIDCode code)
    {
        GameObject tower = new GameObject();

        // TowerData// towerData = tower.AddComponent<TowerData>();           // Item 컴포넌트 추가하기
        // TowerData towerData = TowerDataManager.Inst.TowerData[code];   // towerData 할당

        //string[] itemName = towerData.name.Split("_");      // 00_Ruby => 00 Ruby로 분할
        // tower.name = $"{itemName[1]}_{itemCount++}";        // 오브젝트 이름 설정하기
        tower.layer = LayerMask.NameToLayer("Tower");       // 레이어 설정

        SphereCollider sc = tower.AddComponent<SphereCollider>(); // 컬라이더 추가
        sc.isTrigger = true;
        sc.radius = 0.5f;
        sc.center = Vector3.up;

        return tower;
    }
}
