using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ���Ϳ� �߻�ü�� ������ ������ ü���� ���̴��� �׽�Ʈ �뵵
/// </summary>
public class MonsterBase : MonoBehaviour 
{
    public float monsterHp = 100.0f;    // ���� ü��

    InputActionTower inputAction;       // ��ǲ �׼�

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
        Debug.Log($"{MonsterHp}");  // (�����̽� Ű�� ������ ������
    }
}
