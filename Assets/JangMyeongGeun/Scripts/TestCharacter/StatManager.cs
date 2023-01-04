using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("기본 능력치")]
    [SerializeField]
    private float default_MaxHp = 100;
    public float Default_MaxHp => default_MaxHp;

    [HideInInspector]
    public float extra_MaxHp = 0.0f;


    [SerializeField]
    private float default_Def = 20;

    public float Default_Def => default_Def;

    [HideInInspector]
    public float extra_Def = 0.0f;


    [SerializeField]
    private float default_Atk = 80;

    public float Default_Atk => default_Atk;

    [HideInInspector]
    public float extra_Atk = 0.0f;

    [SerializeField]
    private float default_AtkSpeed = 40.0f;

    public float Default_AtkSpeed => default_AtkSpeed;

    [HideInInspector]
    public float extra_AtkSpeed = 0.0f;

    [SerializeField]
    private float default_CriticalChance = 0.0f;

    public float Default_CriticalChance => default_CriticalChance;

    [HideInInspector]
    public float extra_CriticalChance = 0.0f;

    [SerializeField]
    private float default_MoveSpeed = 30.0f;

    public float Default_MoveSpeed => default_MoveSpeed;

    [HideInInspector]
    public float extra_MoveSpeed = 0.0f;

    Player player;
    Inventory inven;

    public List<ItemSlot> slots;

    public void InitializeStat(Inventory inven)
    {
        player = GameManager.Inst.Player;
        SetPlayerStat();
        ResetExtraStat();
        this.inven = inven;
        inven.onRefreshSlot += RefreshSlots;
        inven.onAddSlotItemEffect = (itemData) => AddEffect(itemData);
        inven.onSubSlotItemEffect = (itemData,itemCount) => SubEffect(itemData,itemCount);
    }

    private void SetPlayerStat()
    {
        player.MAXHP = default_MaxHp;
        player.HP = default_MaxHp;
        player.DEF = default_Def;
        player.ATK = default_Atk;
        player.ATKSpeed = default_AtkSpeed;
        player.CriticalChance = default_CriticalChance;
        player.MoveSpeed = default_MoveSpeed;
    }

    private void AddEffect(ItemData_Base itemData)
    {
        itemData.Effect(this);

        player.MAXHP += extra_MaxHp;
        player.DEF += extra_Def;
        player.ATK += extra_Atk;
        player.ATKSpeed += extra_AtkSpeed;
        player.CriticalChance += extra_CriticalChance;
        player.MoveSpeed += extra_MoveSpeed;

        ResetExtraStat();
    }
    private void SubEffect(ItemData_Base itemData, uint itemCount)
    {
        itemData.Effect(this);

        player.MAXHP -= (extra_MaxHp * itemCount);
        player.DEF -= (extra_Def * itemCount);
        player.ATK -= (extra_Atk * itemCount);
        player.ATKSpeed -= (extra_AtkSpeed * itemCount);
        player.CriticalChance -= (extra_CriticalChance * itemCount);
        player.MoveSpeed -= (extra_MoveSpeed * itemCount);

        ResetExtraStat();
    }


    void ResetExtraStat()
    {
        extra_MaxHp = 0.0f;
        extra_Def = 0.0f;
        extra_Atk = 0.0f;
        extra_AtkSpeed = 0.0f;
        extra_CriticalChance = 0.0f;
        extra_MoveSpeed = 0.0f;
    }

    public void RefreshSlots()
    {
        slots = inven.GetInventorySlotList();
    }
}
