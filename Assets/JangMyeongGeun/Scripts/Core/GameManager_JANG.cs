using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_JANG : Singleton<GameManager_JANG>
{
    ItemManager itemData;
    StatManager statManager;
    InventoryUI inventoryUI;
    Test_Player_JANG player;

    public ItemManager ItemData => itemData;
    public StatManager StatManager => statManager;
    public InventoryUI InventoryUI => inventoryUI;
    
    public Test_Player_JANG Player => player;


    protected override void Initialize()
    {
        itemData = GetComponent<ItemManager>();
        statManager = GetComponent<StatManager>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        player = FindObjectOfType<Test_Player_JANG>();
    }
}
