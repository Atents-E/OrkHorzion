using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Inventory inven;
    public InventoryUI inventoryUI;

    [Header("기본 능력치")]
    [SerializeField]
    private float default_MaxHp = 100;
    [SerializeField]
    private float default_Def = 20;
    [SerializeField]
    private float default_Atk = 80;
    [SerializeField]
    private float default_AtkSpeed = 40.0f;
    [SerializeField]
    private float default_CriticalChance = 0.0f;
    [SerializeField]
    private float default_MoveSpeed = 30.0f;


    private void Awake()
    {
        characterName = "전사";
        maxHp = default_MaxHp;
        hp = maxHp;
        def = default_Def;
        atk = default_Atk;
        atkSpeed = default_AtkSpeed;
        criticalChance = default_CriticalChance;
        moveSpeed = default_MoveSpeed;
    }

    private void Start()
    {
        inven = new Inventory(2);
        GameManager.Inst.InventoryUI.InitializeInventoy(inven);
    }
}
