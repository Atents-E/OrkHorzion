using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_PlayerInventory : Test_Base
{
    Player player;
    InventoryUI invenUI;


    private void Start()
    {
        player = FindObjectOfType<Player>();
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

}
