using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

// 인터페이스 사용
public class TowerCreat : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    // 특정 타워 위의 커서(손가락이) 올라왔는지 확인
    // 올라왔으면 특정 타워의 알파값 0
    // 기본적으로 모든 타워 아이콘의 알파값 1

    // 변수 ------------------------------------------------------------------------------------------------------------
    // 타워 프리팹 생성
    public GameObject towerPrefab;    // 생성 할 타워의 프리팹


    // 델리게이트 --------------------------------------------------------------------------------------------------
    // public Action OnTowerDragStart; // 타워 드래그를 시작했을 때

    private void Awake()
    {
        towerPrefab = GetComponent<GameObject>();
        // OnTowerDragStart += IsInsideTowerIcon();
    }

    private void Start()
    {
        //towerPrefab.onClick.AddListener(MakeTower);
    }

    // 누르면 타워 생성
    public void OnPointerClick(PointerEventData eventData)
    {
        // towerPrefab = Instantiate(towerPrefab, transform);
        Debug.Log("타워 생성");
    }


    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 인터페이스를 사용하기 위한 함수
    }


    // 드래그가 시작되면 타워 프리팹이 활성화 되도록 하기
    //2. 스크롤 뷰를 특정 위치 위에서 나오는지 좌표를 찍기
    public void OnBeginDrag(PointerEventData eventData)
    {
        towerPrefab.SetActive(true);
    }

    // 드롭 했을 때 실행. 타워 위치는 마우스가 드롭 된 위치
    public void OnEndDrag(PointerEventData eventData)
    {
        // 타워 생성 위치는 드롭 위치
        towerPrefab.transform.position = eventData.position;
    }


    //  2.1. 스크롤 뷰의 특정 위치 구하기
    bool IsInsideTowerIcon(Vector2 screenPos)
    {
        RectTransform rectTransform = (RectTransform)transform;

        Vector2 min = new(rectTransform.position.x - rectTransform.sizeDelta.x, rectTransform.position.y - rectTransform.position.y);
        Vector2 max = new(rectTransform.position.x + rectTransform.sizeDelta.x, rectTransform.position.y + rectTransform.position.y);

        return (min.x < screenPos.x && screenPos.x < max.x && min.y < screenPos.y && screenPos.y < max.y);

       // Debug.Log($"{screenPos}");
    }


    //3. 타워 프리팹을 누르면 타워버튼을 알파값을 0으로 하고, 타워 프리팹이 움직이는 순간에 1로 만들어라
    //4. 타워 프리팹은 존재하고 있다가 눌러지면 알파값을 1로 만들어라
    //5. 3~4가 동시에 일어나게 된다면 타워버튼을 눌러서 타워 프리팹이 만들어지고, 이동되는 것 처럼 보일 것 이다.


}
