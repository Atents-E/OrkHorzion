using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    ItemManager itemData;
    InventoryUI inventoryUI;
    Player player;

    public ItemManager ItemData => itemData;
    public InventoryUI InventoryUI => inventoryUI;
    
    public Player Player => player;

    protected override void Initialize()
    {
        itemData = GetComponent<ItemManager>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        player = FindObjectOfType<Player>();
    }
}
