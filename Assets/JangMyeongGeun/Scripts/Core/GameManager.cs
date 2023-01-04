using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    ItemManager itemData;
    StatManager statManager;
    InventoryUI inventoryUI;
    Player player;

    public ItemManager ItemData => itemData;
    public StatManager StatManager => statManager;
    public InventoryUI InventoryUI => inventoryUI;
    
    public Player Player => player;

    protected override void Initialize()
    {
        itemData = GetComponent<ItemManager>();
        statManager = GetComponent<StatManager>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        player = FindObjectOfType<Player>();
    }
}
