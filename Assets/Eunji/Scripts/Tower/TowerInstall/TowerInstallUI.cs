using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TowerInstallUI : MonoBehaviour// , IDragHandler
{
    //// 특정 타워 아이콘 위에서 클릭되는지 확인
    //// 클릭되면, 타워 UI 생성, 생성 완료 된 타워 UI는 마우스를 따라다닌다.
    //// 드래그가 끝면 타워 UI 삭제
    //// 삭제된 위치에서 타워 생성(TowerDataManager에서 생성 할 타워정보를 받아온다.)

    ///// <summary>
    ///// 생성 할 타워 UI
    ///// </summary>
    //public GameObject towerIcon;

    ///// <summary>
    ///// 생성 된 타워 UI
    ///// </summary>
    //private GameObject newTowerIcon;

    //InputActionTower inputTowerMenu;


    ///// <summary>
    ///// 타워가 생성 되었는지 확인하는 델리게이트
    ///// </summary>
    //public Action onTowerIcon; 


    //private void Awake()
    //{
    //    towerIcon = GetComponent<GameObject>();

    //    inputTowerMenu = new InputActionTower();
    //}


    ////private void OnEnable()
    ////{
    ////    inputTowerMenu.Touch.Enable();
    ////    inputTowerMenu.Touch.TouchMenu.performed += OnTowerTouch;
    ////    inputTowerMenu.Touch.TouchMenu.canceled += OnTowerDrop;
    ////}

    ////private void OnDisable()
    ////{
    ////    inputTowerMenu.Touch.TouchMenu.canceled -= OnTowerDrop;
    ////    inputTowerMenu.Touch.TouchMenu.performed -= OnTowerTouch;
    ////    inputTowerMenu.Touch.Disable();
    ////}


    ///// <summary>
    ///// 타워 아이콘 클릭이 시작되면 동작
    ///// </summary>
    ///// <param name="_"></param>
    //private void OnTowerTouch(InputAction.CallbackContext _)
    //{
    //    //Debug.Log("타워가 터치 되었다");

    //    // 타워 UI를 생성
    //    // newTowerIcon = Instantiate(towerIcon, transform);

    //    //if (newTowerIcon != null)
    //    //{
    //    //    Debug.Log("타워가 생성되었다");
    //    //    towerIcon.transform.position = Mouse.current.position.ReadValue();
    //    //}
    //}

    //public void OnDrag(PointerEventData eventData)
    //{

    //}

    ///// <summary>
    ///// 타워 아이콘 클릭이 끝나면 동작
    ///// </summary>
    ///// <param name="context"></param>
    //private void OnTowerDrop(InputAction.CallbackContext context)
    //{
    //    //Debug.Log("타워의 터치가 끝났다");

    //    // TowerDataManager.cs로 생성 할 데이터 확인
    //    // TowerData towerData = TowerDataManager.Inst.towerData[TowerIDCode];
    //    // 드롭 된 위치에 타워 오브젝트를 생성
    //    //Instantiate(towerData.modelPrefab, transform.position, transform.rotation, transform);   // 아이템의 외형 추가

    //    // newTowerIcon 삭제
    //}




    /////// <summary>
    /////// 마우스 이동
    /////// </summary>
    /////// <param name="context"></param>
    ////private void OnMouseMove(InputAction.CallbackContext context)
    ////{
    ////    Vector3 mousePos = context.ReadValue<Vector2>();
    ////    mousePos.z = 10.0f;

    ////    Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

    ////    transform.position = target;
    ////}

    /////<summary>
    /////TowerMenu의 크기
    /////</summary>
    /////<param name = "screenPos" ></ param >
    /////< returns ></ returns >
    ////bool IsInsideTowerMenu(Vector2 screenPos)
    ////{
    ////    RectTransform rectTransform = (RectTransform)transform;

    ////    Vector2 min = new(rectTransform.position.x - rectTransform.sizeDelta.x, rectTransform.position.y - rectTransform.position.y);
    ////    Vector2 max = new(rectTransform.position.x + rectTransform.sizeDelta.x, rectTransform.position.y + rectTransform.position.y);

    ////    return (min.x < screenPos.x && screenPos.x < max.x && min.y < screenPos.y && screenPos.y < max.y);
    ////}

}