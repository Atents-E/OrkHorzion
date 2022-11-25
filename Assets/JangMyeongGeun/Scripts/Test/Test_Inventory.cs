using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Inventory : TestBase
{
    public InventoryUI inventoryUI;
    Inventory inven;

    void Start()
    {
        inven = new Inventory(4);
        inventoryUI.InitializeInventoy(inven);
    }

    protected override void OnTest1(InputAction.CallbackContext obj)
    {
        inven.AddItem(ItemIDCode.ATK1);
    }

    protected override void OnTest2(InputAction.CallbackContext obj)
    {
        inven.AddItem(ItemIDCode.ATK2);
    }

}
