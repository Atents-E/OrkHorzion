using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Inventory : Test_Base_JANG
{
    public InventoryUI inventoryUI;
    public Inventory inven;

    void Start()
    {
        inven = new Inventory(2);
        inventoryUI.InitializeInventoy(inven);
    }

    protected override void OnTest1(InputAction.CallbackContext obj)
    {
        //inven.AddItem(ItemIDCode.ATK1);
    }

    protected override void OnTest2(InputAction.CallbackContext obj)
    {
        //inven.AddItem(ItemIDCode.ATK2);
    }

    protected override void OnTest3(InputAction.CallbackContext obj)
    {
        //inven.AddItem(ItemIDCode.ATK3);
    }

    protected override void OnTest4(InputAction.CallbackContext obj)
    {
        //inven.AddItem(ItemIDCode.Debuff1);
    }

    protected override void OnTest5(InputAction.CallbackContext obj)
    {
        //inven.AddItem(ItemIDCode.Gold1);
    }

    protected override void OnTest6(InputAction.CallbackContext obj)
    {
        inven.AddItem(ItemIDCode.Health1);
    }

    protected override void OnTest8(InputAction.CallbackContext obj)
    {
        inven.IncreaseSlot();
    }

    protected override void OnTest9(InputAction.CallbackContext obj)
    {
        inven.DecreaseSlot(2);
    }
}
