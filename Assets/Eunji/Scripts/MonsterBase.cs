using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 몬스터와 발사체가 만나면 몬스터의 체력이 깍이는지 테스트 용도
/// </summary>
public class MonsterBase : MonoBehaviour 
{
    public float monsterHp = 100.0f;    // 몬스터 체력

    InputActionTower inputAction;       // 인풋 액션

    private void Awake()
    {
        inputAction = new InputActionTower();
    }

    public float MonsterHp
    {
        get => monsterHp;
        set
        {
            monsterHp = value;        
        }
    }

    private void OnEnable()
    {
        inputAction.Test.Enable();
        inputAction.Test.ShowMonsterHP.performed += ShowMonsterHP;   
    }

    private void ShowMonsterHP(InputAction.CallbackContext _)
    {
        Debug.Log($"{MonsterHp}");  // (스페이스 키를 누르면 보여줌
    }
}
