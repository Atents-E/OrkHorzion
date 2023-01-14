using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_PlayerInventory : Test_Base_JANG
{
    Wizard player;
    InventoryUI invenUI;
    private void Start()
    {
        player = FindObjectOfType<Wizard>();
        invenUI = FindObjectOfType<InventoryUI>();
    }

    protected override void OnTest1(InputAction.CallbackContext obj)
    {
        invenUI.InventoryOpenClose(invenUI.IsOpen);
    }

    protected override void OnTest2(InputAction.CallbackContext obj)
    {
        player.inven.AddItem(ItemIDCode.ATK1);
    }

    protected override void OnTest3(InputAction.CallbackContext obj)
    {
        player.inven.AddItem(ItemIDCode.ATK2);
    }

    protected override void OnTest4(InputAction.CallbackContext obj)
    {
        player.inven.AddItem(ItemIDCode.ATK3);
    }

    protected override void OnTest5(InputAction.CallbackContext obj)
    {
        player.inven.AddItem(ItemIDCode.Buff1);
    }

    protected override void OnTest7(InputAction.CallbackContext obj)
    {
        player.inven.IncreaseSlot();
    }

    protected override void OnTest8(InputAction.CallbackContext obj)
    {
        player.inven.DecreaseSlot(2);
    }

    protected override void OnTest9(InputAction.CallbackContext obj)
    {
        Debug.Log($"현재 체력 : {player.HP} / {player.MaxHP}");
        Debug.Log($"현재 공격력 : {player.AttackPower}");
        Debug.Log($"현재 방어력 : {player.DefencePower}");
        Debug.Log($"현재 이동속도 : {player.MoveSpeed}");
        Debug.Log($"현재 치명타확률 : {player.CriticalChance * 100}%");
    }

}
