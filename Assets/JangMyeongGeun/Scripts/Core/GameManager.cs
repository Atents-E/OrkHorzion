using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    ItemManager itemData;
    InventoryUI inventoryUI;

    public ItemManager ItemData => itemData;
    public InventoryUI InventoryUI => inventoryUI;

    protected override void Initialize()
    {
        itemData = GetComponent<ItemManager>();
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

}
