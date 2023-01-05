using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_PlayerInventory : Test_Base_JANG
{
    Test_Player_JANG player;
    InventoryUI invenUI;


    private void Start()
    {
        player = FindObjectOfType<Test_Player_JANG>();
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
        player.inven.AddItem(ItemIDCode.DEF1);
    }

    protected override void OnTest4(InputAction.CallbackContext obj)
    {
        player.inven.AddItem(ItemIDCode.Health1);
    }

    protected override void OnTest5(InputAction.CallbackContext obj)
    {
        player.inven.IncreaseSlot();
    }

    protected override void OnTest6(InputAction.CallbackContext obj)
    {
        player.inven.DecreaseSlot(2);
    }

    protected override void OnTest9(InputAction.CallbackContext obj)
    {
        Debug.Log($"플레이어 체력 : {player.HP} / {player.MAXHP}");
        Debug.Log($"플레이어 공격력 : {player.ATK}");
        Debug.Log($"플레이어 방어력 : {player.DEF}");
        Debug.Log($"플레이어 치명타확률 : {player.CriticalChance * 100}%");
    }

}
