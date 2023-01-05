using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player_JANG : Test_Character_JANG
{
    public Inventory inven;
    StatManager statManager;

    private void Start()
    {
        inven = new Inventory(2);
        statManager = GameManager_JANG.Inst.StatManager;
        statManager.WarriorInitializeStat(inven);
        GameManager_JANG.Inst.InventoryUI.InitializeInventoy(inven);
    }

}
