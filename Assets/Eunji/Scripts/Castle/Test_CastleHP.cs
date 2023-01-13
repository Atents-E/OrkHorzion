using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// Test_CastleHP 기능
// 1. 정상적으로 거점타워의 HP가 줄어드는지 확인
public class Test_CastleHP : MonoBehaviour
{
    /// <summary>
    /// 거점 타워
    /// </summary>
    Castle castle;

    /// <summary>
    /// 인풋 시스템
    /// </summary>
    TowerInputActions inputActions;

    private void Awake()
    {
        castle = GameManager.Inst.Castle;

        inputActions = new TowerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Test.Enable();
        inputActions.Test.Test1.performed += CastleHpChange;
    }

    private void OnDisable()
    {
        inputActions.Test.Test1.performed -= CastleHpChange;
        inputActions.Test.Disable();
    }

    private void CastleHpChange(InputAction.CallbackContext _)
    {
        castle.hp -= 500;
        Debug.Log($"{castle.hp}");
    }
}
