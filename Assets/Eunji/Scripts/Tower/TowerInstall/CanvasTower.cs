using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CanvasTower : MonoBehaviour
{
    GameObject towerDelete;     // TowerDelete 게임 오브젝트

    Button okButton;    // ok버튼
    bool ok = false;    // 삭제 확인용 변수

    public bool OK
    {
        get => ok;
        private set => ok = value;
    }


    private void Awake()
    {
        towerDelete = transform.GetChild(2).gameObject;

        towerDelete.SetActive(false);
        okButton.onClick.AddListener(DeleteCheck);
    }

    public void LookTowerDelete()
    {
        // 타워 삭제 메뉴를 활성화
        towerDelete.SetActive(true);
    }


    void DeleteCheck()
    {
        OK = true;      // 삭제 승낙
    }

}
