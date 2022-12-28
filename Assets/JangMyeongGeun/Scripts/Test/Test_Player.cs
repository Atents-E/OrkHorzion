using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player : MonoBehaviour
{
    public Inventory inven;
    public InventoryUI inventoryUI;

    public int HP = 50;
    public int Atk = 50;
    public int Def = 50;

    private void Start()
    {
        inven = new Inventory(2);
        GameManager.Inst.InventoryUI.InitializeInventoy(inven);
    }



}
