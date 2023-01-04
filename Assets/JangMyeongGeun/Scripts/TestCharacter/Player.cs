using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Inventory inven;
    StatManager statManager;

    private void Start()
    {
        inven = new Inventory(2);
        statManager = GameManager.Inst.StatManager;
        statManager.InitializeStat(inven);
        GameManager.Inst.InventoryUI.InitializeInventoy(inven);
    }
}
