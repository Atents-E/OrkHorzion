using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class Warrior : Character
{
    public Inventory inven; // 해당 Warrior 플레이어가 가질 인벤토리 변수 (보상탭에서 해당 인벤토리를 참조할 목적으로 접근제한자는 public으로 하였음) __ 장명근 작성
    StatManager statManager; // 스탯매니저에서 적용할 스탯을 받기 위한 스탯 매니저 변수 __ 장명근 작성
    InventoryUI invenUI;

    Transform hand_r;
    Collider weaponBlade;

    protected override void Awake()
    {
        base.Awake();
        hand_r = GetComponentInChildren<WeaponPosition>().transform; // 무기가 있는 위치 가져오기
        weaponBlade = hand_r.GetComponentInChildren<Collider>(); // 무기 콜라이더 가져오기
        invenUI = FindObjectOfType<InventoryUI>();
        characterName = "전사";
    }

    protected override void Start()
    {
        inven = new Inventory(2);   // 인벤토리 생성자를 통해 해당 Warrior 플레이어가 가지고 있을 인벤토리 __ 장명근 작성
        statManager = GameManager.Inst.StatManager; // 게임 매니저에서 스탯 매니저를 가져옴 __ 장명근 작성
        statManager.WarriorInitializeStat(inven);   // 스탯 매니저에서 입력한 스탯을 적용하고 가지고 있는 아이템에 따라 스탯이 바뀌기 위해 해당 플레이어의 인벤토리를 인수로 넘김 __ 장명근 작성
        GameManager.Inst.InventoryUI.InitializeInventoy(inven); // 인벤토리 상황을 UI로 띄우기 위해 해당 플레이어의 인벤토리를 인수로 넘김 __ 장명근 작성

        base.Start();

        Debug.Log($"이름 : {characterName}");
        Debug.Log($"체력 : {hp} / {maxHp}");
        Debug.Log($"방어력 : {def}");
        Debug.Log($"공격력 : {atk}");
        Debug.Log($"치명타확률 : {criticalChance*100}%");
        Debug.Log($"이동속도 : {moveSpeed}");

    }

    /// <summary>
    /// 무기가 공격 행동을 할 때 무기의 트리거 켜는 함수
    /// </summary>
    public void WeaponBladeEnable()
    {
        if (weaponBlade != null)
        {
            weaponBlade.enabled = true;
        }
    }

    /// <summary>
    /// 무기가 공격 행동이 끝날 때 무기의 트리거를 끄는 함수
    /// </summary>
    public void WeaponBladeDisable()
    {
        if (weaponBlade != null)
        {
            weaponBlade.enabled = false;
        }
    }

    public override void Attack(IBattle target) => base.Attack(target);
  
    public override void TakeDamage(float damage) => base.TakeDamage(damage);

    public override void Die()
    {
        base.Die();
        inven.PlayerRemoveItem(); // 죽으면 아이템 1개 삭제
    } 
    
    public override void Recover() => base.Recover();
}
