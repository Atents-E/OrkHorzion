using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Canvas : MonoBehaviour, IPointerClickHandler
{
    GameObject towerMenu;       // TowerMenu 게임 오브젝트
    GameObject towerDelete;     // TowerDelete 게임 오브젝트

    TowerDelete towerDelete_script;

    private void Awake()
    {
        // 자식으로 할당
        towerMenu = transform.GetChild(1).gameObject;
        towerDelete = transform.GetChild(2).gameObject;
        towerDelete_script = towerDelete.GetComponent<TowerDelete>();   

        // 게임이 시작되면 처음엔 비활성화
        towerMenu.gameObject.SetActive(false);
        towerDelete.gameObject.SetActive(false);
    }

    // 클릭되면
    // 타워인지 확인하고
    // 타워삭제 창을 띄움
    // 삭제가 요청되면
    // 해당 타워의 삭제 함수를 실행
    public void OnPointerClick(PointerEventData eventData)
    {
    //    Camera mainCamera = GameManager.Inst.MainCamera;
    //    Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);   // 스크린 좌표로 레이 생성
        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f)) // 레이와 땅의 충돌 여부 확
            //if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f))
        {
            Debug.Log($"{ray}");
            if (hit.collider.gameObject == CompareTag("Tower"))
            {
                towerDelete.gameObject.SetActive(true);
                GameObject tower = hit.collider.gameObject;
                TowerBase thisTower = tower.transform.GetComponent<TowerBase>();

                if (towerDelete_script.OK != false)     // 삭제 승낙이 되었으면
                {
                    thisTower.DeleteTower();
                }
            }
        }
    }
}
