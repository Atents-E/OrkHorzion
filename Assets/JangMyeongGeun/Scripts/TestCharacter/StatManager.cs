using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static StatManager;

public class StatManager : MonoBehaviour
{
    // 1. Player 기본 스탯
    // 2. 인벤토리 리스트
    // 3. 변경값

    // 1. 인벤토리에서 아이템이 추가 삭제 될 때의 신호를 받는다
    // 2. 받은 신호를 통해서 스탯 갱신 함수를 실행한다
    // 3. 이 때 스탯 갱신 함수안에서는 어떤 아이템들이 있는지를 따져 스탯을 변경해야 한다.

    // 1. 인벤토리에서 아이템이 추가 삭제 될 때의 신호를 받고 인벤토리가 가지고있는 새 리스트를 갱신 받는다. (인벤토리 리스트를 받을 그릇이 필요)
    // 2. 리스트를 갱신 받으면 해당 리스트를 참조하면 된다.

    // 1. 인벤토리 리스트에 변화가 있을 때마다의 이벤트함수에 등록하여 효과를 적용한다.

    [Header("전사 기본 능력치")]
    [SerializeField]
    private float warrior_default_MaxHp = 100.0f;
    [SerializeField]
    private float warrior_default_Def = 100.0f;
    [SerializeField]
    private float warrior_default_Atk = 100.0f;
    [SerializeField]
    private float warrior_default_CriticalChance = 0.3f;
    [SerializeField]
    private float warrior_default_MoveSpeed = 3.0f;

    [Header("마법사 기본 능력치")]
    [SerializeField]
    private float wizard_default_MaxHp = 80.0f;
    [SerializeField]
    private float wizard_default_Def = 80.0f;
    [SerializeField]
    private float wizard_default_Atk = 80.0f;
    [SerializeField]
    private float wizard_default_CriticalChance = 0.3f;
    [SerializeField]
    private float wizard_default_MoveSpeed = 5.0f;

    [HideInInspector]
    public float Default_MaxHp = 0.0f;
    [HideInInspector]
    public float Default_Def = 0.0f;
    [HideInInspector]
    public float Default_Atk = 0.0f;
    [HideInInspector]
    public float Default_CriticalChance = 0.0f;
    [HideInInspector]
    public float Default_MoveSpeed = 0.0f;

    [HideInInspector]
    public float extra_MaxHp = 0.0f;
    [HideInInspector]
    public float extra_Def = 0.0f;
    [HideInInspector]
    public float extra_Atk = 0.0f;
    [HideInInspector]
    public float extra_CriticalChance = 0.0f;
    [HideInInspector]
    public float extra_MoveSpeed = 0.0f;

    Inventory inven;

    public List<ItemSlot> slots;

    public void WarriorInitializeStat(Inventory inven)
    {
        Warrior warrior = GameManager.Inst.Warrior;
        SetWarriorStat(warrior);

        ResetExtraStat();

        this.inven = inven;
        inven.onRefreshSlot += RefreshSlots;
        inven.onAddSlotItemEffect = (itemData) => AddEffect(warrior, itemData);
        inven.onSubSlotItemEffect = (itemData, itemCount) => SubEffect(warrior, itemData, itemCount);
    }

    public void WizardInitializeStat(Inventory inven)
    {
        Wizard wizard = GameManager.Inst.Wizard;
        SetWizardStat(wizard);

        ResetExtraStat();

        this.inven = inven;
        inven.onRefreshSlot += RefreshSlots;
        inven.onAddSlotItemEffect = (itemData) => AddEffect(wizard, itemData);
        inven.onSubSlotItemEffect = (itemData, itemCount) => SubEffect(wizard, itemData, itemCount);
    }

    private void SetWarriorStat(Character warrior)
    {
        Default_MaxHp = warrior_default_MaxHp;
        Default_Def = warrior_default_Def;
        Default_Atk = warrior_default_Atk;
        Default_CriticalChance = warrior_default_CriticalChance;
        Default_MoveSpeed = warrior_default_MoveSpeed;

        warrior.ChangeStat(
            Default_MaxHp,
            Default_Atk,
            Default_Def,
            Default_MoveSpeed,
            Default_CriticalChance
            );
    }

    private void SetWizardStat(Character wizard)
    {
        Default_MaxHp = wizard_default_MaxHp;
        Default_Def = wizard_default_Def;
        Default_Atk = wizard_default_Atk;
        Default_CriticalChance = wizard_default_CriticalChance;
        Default_MoveSpeed = wizard_default_MoveSpeed;

        wizard.ChangeStat(
            Default_MaxHp,
            Default_Atk,
            Default_Def,
            Default_MoveSpeed,
            Default_CriticalChance
            );
    }

    private void AddEffect(Character character, ItemData_Base itemData)
    {
        itemData.Effect(this);

        character.AddEffect(
            extra_MaxHp,
            extra_Atk,
            extra_Def,
            extra_MoveSpeed,
            extra_CriticalChance
            );

        ResetExtraStat();
    }
    private void SubEffect(Character character, ItemData_Base itemData, uint itemCount)
    {
        itemData.Effect(this);

        character.SubEffect(
            extra_MaxHp * itemCount,
            extra_Atk * itemCount,
            extra_Def * itemCount,
            extra_MoveSpeed * itemCount,
            extra_CriticalChance * itemCount
            );
        ResetExtraStat();
    }


    void ResetExtraStat()
    {
        extra_MaxHp = 0.0f;
        extra_Def = 0.0f;
        extra_Atk = 0.0f;
        extra_CriticalChance = 0.0f;
        extra_MoveSpeed = 0.0f;
    }

    public void RefreshSlots()
    {
        slots = inven.GetInventorySlotList();
    }
}