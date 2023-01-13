using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Character character;
    Warrior warrior;
    Wizard wizard;

    ItemManager itemData;
    StatManager statManager;
    InventoryUI inventoryUI;
    RewardPanel rewardPanel;

    public ItemManager ItemData => itemData;
    public StatManager StatManager => statManager;
    public InventoryUI InventoryUI => inventoryUI;

    public RewardPanel RewardPanel => rewardPanel;
    //Camera mainCamera; 

    public Character Character => character;
    public Warrior Warrior => warrior;   
    public Wizard Wizard => wizard;

    //public Camera MainCamera => mainCamera;

    protected override void Initialize()
    {
        character = FindObjectOfType<Character>();
        warrior = FindObjectOfType<Warrior>();
        wizard = FindObjectOfType<Wizard>();
        itemData = GetComponent<ItemManager>();
        statManager = GetComponent<StatManager>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        rewardPanel = FindObjectOfType<RewardPanel>();

        //mainCamera = FindObjectOfType<Camera>();
    }
}
