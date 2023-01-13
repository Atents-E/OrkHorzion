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
    TowerMenuPanel towerMenuPanel;
    RewardPanel rewardPanel;

    PlayerGold playerGold;
    Canvas canvas;
    Castle castle;

    EnemySpawner enemySpawner;
    WaveSystem waveSystem;

    public Character Character => character;
    public Warrior Warrior => warrior;   
    public Wizard Wizard => wizard;

    public ItemManager ItemData => itemData;
    public StatManager StatManager => statManager;
    public InventoryUI InventoryUI => inventoryUI;
    public TowerMenuPanel TowerMenuPanel => towerMenuPanel;

    public RewardPanel RewardPanel => rewardPanel;

    public PlayerGold PlayerGold => playerGold;
    public Canvas Canvas => canvas;
    public Castle Castle => castle;

    public EnemySpawner EnemySpawner => enemySpawner;
    public WaveSystem WaveSystem => waveSystem;

    protected override void Initialize()
    {
        character = FindObjectOfType<Character>();
        warrior = FindObjectOfType<Warrior>();
        wizard = FindObjectOfType<Wizard>();
        itemData = GetComponent<ItemManager>();
        statManager = GetComponent<StatManager>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        towerMenuPanel = FindObjectOfType<TowerMenuPanel>();
        rewardPanel = FindObjectOfType<RewardPanel>();
        playerGold = FindObjectOfType<PlayerGold>();
        canvas = FindObjectOfType<Canvas>();
        castle = FindObjectOfType<Castle>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        waveSystem = FindObjectOfType<WaveSystem>();
    }
}
