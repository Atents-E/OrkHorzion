using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class CanvasTower : MonoBehaviour
{
    GameObject deletePanel;     // TowerDelete 게임 오브젝트
    GameObject button_OK;       // 삭제 확인 버튼

    Button okButton;            // ok버튼

    /// <summary>
    /// 인풋 시스템
    /// </summary>
    protected TowerInputActions inputActions;

    private void Awake()
    {
        deletePanel = transform.GetChild(2).gameObject;
        button_OK = transform.GetChild(2).GetChild(2).gameObject;
        deletePanel.SetActive(false);

        okButton = button_OK.GetComponentInChildren<Button>();

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
            // Debug.Log($"{hit.collider.name}");

            // 2. 삭제 패널을 활성화 하라고 신호를 보내고
            deletePanel.SetActive(true);

            // 3. 삭제 승낙이 되었으면 타워 가격의 일부를 반환하고, 타워 삭제
            TowerBase thisTower = hit.collider.gameObject.GetComponent<TowerBase>();
            okButton.onClick.AddListener(() => thisTower.DeleteTower());
        }
    }
}
