using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

// Tower_Delete.cs의 기능
// 1. 게임 시작되면 삭제 패널 비활성화
// 2. 게임 내 설치 된 타워를 선택하면 삭제 패널 활성화
// 3. ok버튼이 눌러지면 선택되었던 타워를 삭제(해당 타워 script에 들어있는 DeleteTower() 함수 실행
// 4. no버튼이 패널 비활성화 

public class Tower_Delete : MonoBehaviour
{
    /// <summary>
    /// 삭제 승낙 버튼
    /// </summary>
    Button okButton;           

    /// <summary>
    /// 삭제 취소 버튼
    /// </summary>
    Button noButton; 

    /// <summary>%
    /// 선택한 타워
    /// </summary>
    TowerBase thisTower;

    /// <summary>
    /// 인풋 시스템
    /// </summary>
    protected TowerInputActions inputActions;

    private void Awake()
    {
        noButton = transform.GetChild(1).GetComponentInChildren<Button>();
        okButton = transform.GetChild(2).GetComponentInChildren<Button>();

        noButton.onClick.AddListener(() => gameObject.SetActive(false));  // no 버튼이 눌리면 패널 다시 비활성화
        okButton.onClick.AddListener(() => thisTower.DeleteTower());                // ok 버튼이 눌리면 타워 삭제

        inputActions = new TowerInputActions();
    }

    protected void OnEnable()
    {
        inputActions.Tower.Enable();
        inputActions.Tower.Remove.performed += OnRemove;
    }

    protected void OnDisable()
    {
        inputActions.Tower.Remove.performed -= OnRemove;
        inputActions.Tower.Disable();
    }

    /// <summary>
    /// 타워를 선택하고
    /// ok(삭제 확인)버튼을 클릭하면 삭제
    /// </summary>
    /// <param name="_"></param>
    public void OnRemove(InputAction.CallbackContext _)
    {
        Vector3 selectedTower = Mouse.current.position.ReadValue();         // 마우스 위치 받아오고(마우스 위치는 스크린 좌표계)
        selectedTower.z = -10;

        Ray ray = Camera.main.ScreenPointToRay(selectedTower);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, LayerMask.GetMask("Tower"), QueryTriggerInteraction.Ignore))
        {
            // 1. 충돌한 지점에 오브젝트가 있다
             Debug.Log($"{hit.collider.name}");

            // 2. 삭제 패널을 활성화 하라고 신호를 보내고
            transform.gameObject.SetActive(true);

            // 3. 삭제 승낙이 되었으면 타워 가격의 일부를 반환하고, 타워 삭제
            thisTower = hit.collider.gameObject.GetComponent<TowerBase>();
        }
    }

}
