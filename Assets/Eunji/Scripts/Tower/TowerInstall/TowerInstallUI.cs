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
    //// Ư�� Ÿ�� ������ ������ Ŭ���Ǵ��� Ȯ��
    //// Ŭ���Ǹ�, Ÿ�� UI ����, ���� �Ϸ� �� Ÿ�� UI�� ���콺�� ����ٴѴ�.
    //// �巡�װ� ���� Ÿ�� UI ����
    //// ������ ��ġ���� Ÿ�� ����(TowerDataManager���� ���� �� Ÿ�������� �޾ƿ´�.)

    ///// <summary>
    ///// ���� �� Ÿ�� UI
    ///// </summary>
    //public GameObject towerIcon;

    ///// <summary>
    ///// ���� �� Ÿ�� UI
    ///// </summary>
    //private GameObject newTowerIcon;

    //InputActionTower inputTowerMenu;


    ///// <summary>
    ///// Ÿ���� ���� �Ǿ����� Ȯ���ϴ� ��������Ʈ
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
    ///// Ÿ�� ������ Ŭ���� ���۵Ǹ� ����
    ///// </summary>
    ///// <param name="_"></param>
    //private void OnTowerTouch(InputAction.CallbackContext _)
    //{
    //    //Debug.Log("Ÿ���� ��ġ �Ǿ���");

    //    // Ÿ�� UI�� ����
    //    // newTowerIcon = Instantiate(towerIcon, transform);

    //    //if (newTowerIcon != null)
    //    //{
    //    //    Debug.Log("Ÿ���� �����Ǿ���");
    //    //    towerIcon.transform.position = Mouse.current.position.ReadValue();
    //    //}
    //}

    //public void OnDrag(PointerEventData eventData)
    //{

    //}

    ///// <summary>
    ///// Ÿ�� ������ Ŭ���� ������ ����
    ///// </summary>
    ///// <param name="context"></param>
    //private void OnTowerDrop(InputAction.CallbackContext context)
    //{
    //    //Debug.Log("Ÿ���� ��ġ�� ������");

    //    // TowerDataManager.cs�� ���� �� ������ Ȯ��
    //    // TowerData towerData = TowerDataManager.Inst.towerData[TowerIDCode];
    //    // ��� �� ��ġ�� Ÿ�� ������Ʈ�� ����
    //    //Instantiate(towerData.modelPrefab, transform.position, transform.rotation, transform);   // �������� ���� �߰�

    //    // newTowerIcon ����
    //}




    /////// <summary>
    /////// ���콺 �̵�
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
    /////TowerMenu�� ũ��
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