using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerInstallUI : MonoBehaviour, IDragHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
{
    // 강사님 피드백(11.11) 
    //1. InputSystem으로 피킹

    public GameObject towerPrefab;


    // 델리게이트 --------------------------------------------------------------------------------------------------
    public Action OnTowerDragStart; // 타워 드래그를 시작했을 때


    // 함수 --------------------------------------------------------------------------------------------------------
    private void Awake()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        // UnityEngine.EventSystems 인터페이스를 사용하기 위한 함수
        OnTowerDragStart?.Invoke();
    }

    //2. 스크롤 뷰를 특정 위치 위에서 나오는지 좌표를 찍기
    //  2.1. 스크롤 뷰의 특정 위치 구하기
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그를 시작 했을 때 실행
        // 드래그가 시작되면 프리팹의 알파값 조정
    }

    // 드롭 했을 때(드래그가 끝났을 때) 실행
    public void OnEndDrag(PointerEventData eventData)
    {

    }

    bool IsInsideTowerButton(Vector2 screenPos)
    {
        RectTransform rectTransform = (RectTransform)transform;

        Vector2 min = new(rectTransform.position.x - rectTransform.sizeDelta.x, rectTransform.position.y - rectTransform.position.y);
        Vector2 max = new(rectTransform.position.x + rectTransform.sizeDelta.x, rectTransform.position.y + rectTransform.position.y);

        return (min.x < screenPos.x && screenPos.x < max.x && min.y < screenPos.y && screenPos.y < max.y);
    }


    //3. 타워 프리팹을 누르면 타워버튼을 알파값을 0으로 하고, 타워 프리팹이 움직이는 순간에 1로 만들어라
    //4. 타워 프리팹은 존재하고 있다가 눌러지면 알파값을 1로 만들어라
    //5. 3~4가 동시에 일어나게 된다면 타워버튼을 눌러서 타워 프리팹이 만들어지고, 이동되는 것 처럼 보일 것 이다.

    // 마우스 커서가 해당 위에 온다면 실행
    public void OnPointerClick(PointerEventData eventData)
    {

    }
}